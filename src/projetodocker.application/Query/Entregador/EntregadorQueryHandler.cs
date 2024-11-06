using MediatR;
using Domain;
using Domain.Entity;
using Flunt.Notifications;

namespace Application
{
    public class EntregadorQueryHandler : Notifiable<Notification>,
                                 IRequestHandler<EntregadorQuery, CommandResult>
    {
        private readonly IRepositoryBase<Entregador> repository;
        private readonly INotificationContext _notificationContext;

        public EntregadorQueryHandler(IRepositoryBase<Entregador> repository, INotificationContext notificationContext)
        {
            this.repository = repository;
            _notificationContext = notificationContext;
        }

        public async Task<CommandResult> Handle(EntregadorQuery request, CancellationToken cancellationToken)
        {
            var result = new List<EntregadorResult>();
            var listEntity = repository.GetAll();

            foreach (var entity in listEntity)
            {
                EntregadorResult view = EntregadorResult.Map(entity);
                result.Add(view);
            }
            return new CommandResult(null, result);
        }

    }
}