using Enums;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Command
{
    public class MotoRemoverComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public MotoRemoverComand(int id)
        {
            Id = id;

            AddNotifications(new Contract<MotoRemoverComand>()
                .Requires()
                .IsGreaterThan(Id, 0, "Id", "Id da moto não informado!")
            );
        }

        public int Id { get; set; }

        public MotoDeleted Map()
        {
            return new MotoDeleted
            {
                Id = Id
            };
        }
    }
}
