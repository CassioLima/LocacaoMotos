using MediatR;
using Domain;
using Domain.Entity;
using Flunt.Notifications;

namespace Application
{
    public class MotoQueryHandler : Notifiable<Notification>,
                                 IRequestHandler<MotoQuery, CommandResult>,
                                 IRequestHandler<MotoQueryById, CommandResult>
    {
        private readonly IRepositoryBase<Moto> repository;
        private readonly INotificationContext _notificationContext;

        public MotoQueryHandler(IRepositoryBase<Moto> repository, INotificationContext notificationContext)
        {
            this.repository = repository;
            _notificationContext = notificationContext;
        }

        public async Task<CommandResult> Handle(MotoQuery request, CancellationToken cancellationToken)
        {
            var result = new List<MotoResult>();

            var ListEntity = repository.GetAll();

            foreach (var entity in ListEntity)
            {
                MotoResult view = MotoResult.Map(entity);
                result.Add(view);
            }
            return new CommandResult(true, null, result);
        }

        public async Task<CommandResult> Handle(MotoQueryById request, CancellationToken cancellationToken)
        {
            var entity = repository.GetById(request.Id);
            if (entity == null)
                return new CommandResultError("Moto não encontrada");

            return new CommandResult(true, null, MotoResult.Map(entity));
        }

    }
}