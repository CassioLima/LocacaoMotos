using Domain;
using Domain.Entity;
using Flunt.Notifications;
using MassTransit;
using MediatR;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Command
{
    public class LocacaoCommandHandler : Notifiable<Notification>,
                                            IRequest<CommandResult>,
                                            IRequestHandler<LocacaoCriarComand, CommandResult>,
                                            IRequestHandler<LocacaoDevolucaoComand, CommandResult>
    {
        private readonly INotificationContext _notificationContext;
        private readonly IRepositoryBase<Locacao> _repository;
        private readonly IRepositoryBase<Plano> _repositoryPlano;
        private readonly IPublishEndpoint _publishEndpoint;

        public LocacaoCommandHandler(INotificationContext notificationContext, IRepositoryBase<Locacao> repository, IPublishEndpoint publishEndpoint, IRepositoryBase<Plano> repositoryPlano)
        {
            this._notificationContext = notificationContext;
            this._repository = repository;
            this._publishEndpoint = publishEndpoint;
            _repositoryPlano = repositoryPlano;
        }

        public async Task<CommandResult> Handle(LocacaoCriarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notificationContext.AddNotification(request.Notifications);
                return new CommandResult();
            }

            await _publishEndpoint.Publish<AlugarMotoCreated>(request.Map());

            return new CommandResult(true, "Locação criada com sucesso!", null);
        }

        public async Task<CommandResult> Handle(LocacaoDevolucaoComand request, CancellationToken cancellationToken)
        {
            var entity = _repository.GetById(request.Id);
            var entityPlano = _repositoryPlano.GetById(entity.PlanoId);

            DateTime dataPrevisaoTermino = entity.DataInicio.AddDays(entityPlano.QuantidadeDias);

            var valorLocacao = CalcularValorLocacao(entityPlano.QuantidadeDias, entityPlano.Valor, entity.DataInicio, request.DataTermino, entity.DataInicio, dataPrevisaoTermino);

            if (request.DataTermino < entity.DataCriacao)
                return new CommandResultError("Data de término não pode ser maior do que a data de inicio!");

            await _publishEndpoint.Publish<InformarDataDevolucaoCreated>(request.Map());

            return new CommandResult(true, $"Devolução feita com sucesso, seu valor é R$ {valorLocacao:F2}!", null);
        }

        public decimal CalcularValorLocacao(int diasPlano, decimal valorDiario, DateTime dataCriacao, DateTime dataDevolucao, DateTime dataInicio, DateTime dataPrevisaoTermino)
        {
            // Define os parâmetros do plano
            decimal multaPorcentagem = diasPlano switch
            {
                7 => 0.20m,
                15 => 0.40m,
                _ => 0.00m
            };

            const decimal multaDiariaAtraso = 50.00m;

            // Calcula o valor total básico da locação
            decimal valorTotal = valorDiario * diasPlano;

            // Se a devolução for antecipada (antes da data prevista de término)
            if (dataDevolucao < dataPrevisaoTermino)
            {
                int diasNaoEfetuados = (dataPrevisaoTermino - dataDevolucao).Days;
                decimal valorNaoEfetuado = diasNaoEfetuados * valorDiario;
                decimal multa = valorNaoEfetuado * multaPorcentagem;
                valorTotal = valorTotal - valorNaoEfetuado + multa;
            }
            // Se a devolução for atrasada (após a data prevista de término)
            else if (dataDevolucao > dataPrevisaoTermino)
            {
                int diasAdicionais = (dataDevolucao - dataPrevisaoTermino).Days;
                decimal valorAtraso = diasAdicionais * multaDiariaAtraso;
                valorTotal += valorAtraso;
            }

            return valorTotal;
        }



    }
}