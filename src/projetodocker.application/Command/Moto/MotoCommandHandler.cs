using Domain;
using Domain.Entity;
using Flunt.Notifications;
using MassTransit;
using MediatR;
using Shared.Messages;

namespace Application.Command
{
    public class MotoCommandHandler : Notifiable<Notification>,
                                        IRequest<CommandResult>,
                                        IRequestHandler<MotoCriarComand, CommandResult>,
                                        IRequestHandler<MotoRemoverComand, CommandResult>,
                                        IRequestHandler<MotoAtualizarComand, CommandResult>

    {
        private readonly INotificationContext _notificationContext;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRepositoryBase<Moto> repository;
        private readonly IRepositoryBase<Locacao> repositoryLocacao;

        public MotoCommandHandler(INotificationContext notificationContext, IPublishEndpoint publishEndpoint, IRepositoryBase<Moto> repository, IRepositoryBase<Locacao> repositoryLocacao)
        {
            this._notificationContext = notificationContext;
            this._publishEndpoint = publishEndpoint;
            this.repository = repository;
            this.repositoryLocacao = repositoryLocacao;
        }

        public async Task<CommandResult> Handle(MotoCriarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notificationContext.AddNotification(request.Notifications);
                return new CommandResult();
            }

            if (request.Ano == 2024)
                await _publishEndpoint.Publish<Moto2024Created>(request.Map2024());

            await _publishEndpoint.Publish<MotoCreated>(request.Map());

            return new CommandResult(true, "Moto criada com sucesso!", request.Map());
        }

        public async Task<CommandResult> Handle(MotoRemoverComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notificationContext.AddNotification(request.Notifications);
                return new CommandResultError(_notificationContext.Notifications.FirstOrDefault().Message);
            }

            var entity = repository.GetById(request.Id);
            if (entity == null)
                return new CommandResultError("Moto não encontrada");

            var countLocacoes = repositoryLocacao.GetAll().Where(x => x.MotoId == entity.Id).Count();
            if (countLocacoes > 0)
                return new CommandResultError("Moto não pode ser excluida, locações encontradas");

            await _publishEndpoint.Publish<MotoDeleted>(request.Map());

            return new CommandResult(true, "Moto Apagada", null);
        }

        public async Task<CommandResult> Handle(MotoAtualizarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notificationContext.AddNotification(request.Notifications);
                return new CommandResultError(_notificationContext.Notifications.FirstOrDefault().Message);
            }

            Moto moto = this.repository.GetById(request.Id);
            if (moto == null)
                return new CommandResultError("Moto não encontrada");

            await _publishEndpoint.Publish<MotoUpdated>(request.Map());

            return new CommandResult(true, "Atualização realizada com sucesso", null);
        }


    }
}
