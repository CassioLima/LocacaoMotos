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
    public class LocacaoDevolucaoComand : Notifiable<Notification>, IRequest<CommandResult>
    {

        public LocacaoDevolucaoComand(int id, DateTime dataTermino)
        {
            Id = id;
            DataTermino = dataTermino;

            AddNotifications(new Contract<LocacaoAtualizarComand>()
                .Requires()
                .IsGreaterThan(Id, 0, "Id", "Id da locação não informado!")
                .IsNull(dataTermino, "DataTermino", "Data de término não informada!")
            );
        }

        public int Id { get; set; }
        public DateTime DataTermino { get; set; }

        public InformarDataDevolucaoCreated Map()
        {
            return new InformarDataDevolucaoCreated
            {
                Id = Id,
                DataTermino = DataTermino
            };
        }
    }
}
