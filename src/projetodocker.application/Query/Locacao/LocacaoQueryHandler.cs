using MediatR;
using Domain;
using Domain.Entity;
using Flunt.Notifications;

namespace Application
{
    public class LocacaoQueryHandler : Notifiable<Notification>,
                                 IRequestHandler<LocacaoQuery, CommandResult>,
                                 IRequestHandler<LocacaoQueryById, CommandResult>
        
    {
        private readonly IRepositoryBase<Locacao> repository;
        private readonly INotificationContext _notificationContext;

        public LocacaoQueryHandler(IRepositoryBase<Locacao> repository, INotificationContext notificationContext)
        {
            this.repository = repository;
            _notificationContext = notificationContext;
        }

        public async Task<CommandResult> Handle(LocacaoQuery request, CancellationToken cancellationToken)
        {
            var result = new List<LocacaoResult>();

            var listEntity = repository.GetAll();

            foreach (var entity in listEntity)
            {
                LocacaoResult view = LocacaoResult.Map(entity);
                result.Add(view);
            }
            return new CommandResult(null, result);
        }

        public async Task<CommandResult> Handle(LocacaoQueryById request, CancellationToken cancellationToken)
        {
            var entity = repository.GetById(request.Id);
            if (entity == null)
                return new CommandResultError("Locação não encontrada");

            var result = LocacaoResult.Map(entity);

            return new CommandResult(null, result);
        }

    }
}