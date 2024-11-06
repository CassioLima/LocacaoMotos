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
    public class MotoAtualizarComand : Notifiable<Notification>, IRequest<CommandResult>
    {

        public MotoAtualizarComand(int id, string placa)
        {
            Id = id;
            Placa = placa;

            AddNotifications(new Contract<MotoAtualizarComand>()
                .Requires()
                .IsGreaterThan(Id, 0, "Id", "Id da moto não informado!")
                .IsNotNullOrWhiteSpace(Placa, "Placa", "Placa não informada!")
            );
        }

        public int Id { get; set; }
        public string Placa { get; set; }

        public MotoUpdated Map()
        {
            return new MotoUpdated
            {
                Id = Id,
                Placa = Placa
            };
        }
    }
}
