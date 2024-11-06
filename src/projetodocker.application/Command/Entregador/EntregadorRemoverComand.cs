using Enums;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Command
{
    public class EntregadorRemoverComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public EntregadorRemoverComand(int id)
        {
            Id = id;

            AddNotifications(new Contract<EntregadorRemoverComand>()
                .Requires()
                .IsGreaterThan(Id, 0, "Id", "Id do entregador não informado!")
            );
        }

        public int Id { get; set; }
        public int UsuarioId { get; set; }

    }
}
