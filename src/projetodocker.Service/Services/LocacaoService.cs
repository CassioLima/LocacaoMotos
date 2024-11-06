using Application;
using Domain.Entity;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Application.Command;
using Shared.Messages;

namespace Services
{
    public class LocacaoService : ILocacaoService
    {

        private readonly IRepositoryBase<Locacao> repository;
        private readonly IRepositoryBase<Plano> repositoryPlano;
        private readonly INotificationContext _notificationContext;

        public LocacaoService(IRepositoryBase<Locacao> repository, INotificationContext notificationContext, IRepositoryBase<Plano> repositoryPlano)
        {
            this.repository = repository;
            this.repositoryPlano = repositoryPlano;
            _notificationContext = notificationContext;
        }

        public void AlugarMoto(AlugarMotoCreated alugarMotoCreated)
        {
            LocacaoCriarComand request = new LocacaoCriarComand(alugarMotoCreated.MotoId, alugarMotoCreated.DataInicio, alugarMotoCreated.DataPrevisaoTermino, alugarMotoCreated.PlanoId, alugarMotoCreated.EntregadorId);

            if (!request.IsValid)
            {
                _notificationContext.AddNotification(request.Notifications);
                return;
            }

            Locacao locacao = new Locacao
            {
                MotoId = request.Moto_Id,
                EntregadorId = request.Entregador_Id,
                PlanoId = request.Plano_Id,
                DataCriacao = DateTime.Now.AddDays(1).ToUniversalTime(),
                DataInicio = request.Data_Inicio.ToUniversalTime(),
                DataPrevisaoTermino = request.Data_Previsao_Termino.ToUniversalTime()
            };

            this.repository.Save(locacao);
        }

        public void InformarDevolucaoCalcularValor(InformarDataDevolucaoCreated request)
        {
            throw new NotImplementedException();
        }

    }
}
