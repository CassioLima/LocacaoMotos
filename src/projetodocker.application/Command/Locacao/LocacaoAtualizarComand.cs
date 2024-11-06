using Enums;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Command
{
    public class LocacaoAtualizarComand : Notifiable<Notification>, IRequest<CommandResult>
    {

        public LocacaoAtualizarComand(int id, int motoId, DateTime dataInicio, DateTime dataPrevisaoTermino, DateTime? dataTermino)
        {
            Id = id;
            MotoId = motoId;
            DataInicio = dataInicio;
            DataPrevisaoTermino = dataPrevisaoTermino;
            DataTermino = dataTermino;

            AddNotifications(new Contract<LocacaoAtualizarComand>()
                .Requires()
                .IsGreaterThan(Id, 0, "Id", "Id da locação não informado!")
                .IsGreaterThan(MotoId, 0, "MotoId", "Id da moto não informado!")
                .IsLowerOrEqualsThan(DataInicio, DateTime.Today, "DataInicio", "Data de início inválida!")
                .IsGreaterThan(DataPrevisaoTermino, DataInicio, "DataPrevisaoTermino", "Data de previsão de término deve ser após a data de início!")
                //.IfNotNull(DataTermino, c => c.IsGreaterThan(DataTermino.Value, DataInicio, "DataTermino", "Data de término deve ser após a data de início!"))
            );
        }

        public int Id { get; set; }
        public int MotoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }
        public DateTime? DataTermino { get; set; }



    }
}
