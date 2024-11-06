using Domain;
using Domain.Entity;
using Flunt.Notifications;
using MassTransit;
using MassTransit.Transports;
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
    public class EntregadorCommandHandler : Notifiable<Notification>,
                                            IRequest<CommandResult>,
                                            IRequestHandler<EntregadorCriarComand, CommandResult>,
                                            IRequestHandler<EntregadorFotoCriarComand, CommandResult>
        
    {
        private readonly INotificationContext _notificationContext;
        private readonly IRepositoryBase<Entregador> repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public EntregadorCommandHandler(INotificationContext notificationContext, IRepositoryBase<Entregador> repository, IPublishEndpoint publishEndpoint)
        {
            _notificationContext = notificationContext;
            this.repository = repository;
            this._publishEndpoint = publishEndpoint;
        }

        public async Task<CommandResult> Handle(EntregadorFotoCriarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notificationContext.AddNotification(request.Notifications);
                return new CommandResult();
            }

            await _publishEndpoint.Publish<EntregadorFotoCreated>(request.Map());

            return new CommandResult(true, "Foto Enviada com sucesso!", null);
        }

        public async Task<CommandResult> Handle(EntregadorCriarComand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notificationContext.AddNotification(request.Notifications);
                return new CommandResultError(_notificationContext.Notifications.FirstOrDefault().Message);
            }


            var countCnh = repository.GetAll().Where(x => x.NumeroCNH == request.Numero_CNH).Count();
            if (countCnh > 0)
                return new CommandResultError("Numero da CNH já cadastrada");

            var countCnpj = repository.GetAll().Where(x => x.CNPJ == request.CNPJ).Count();
            if (countCnpj > 0)
                return new CommandResultError("Numero do CNPJ já cadastrado");

            await _publishEndpoint.Publish<EntregadorCreated>(request.Map());

            return new CommandResult(true, "Entregador criado com sucesso!", null);
        }


    }
}
