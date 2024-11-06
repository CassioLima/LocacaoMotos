using Domain.Entity;
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
    public class LocacaoCriarComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public LocacaoCriarComand(int motoId, DateTime dataInicio, DateTime dataPrevisaoTermino, int planoId, int entregadorId)
        {
            Moto_Id = motoId;
            Data_Inicio = dataInicio;
            Data_Previsao_Termino = dataPrevisaoTermino;
            Entregador_Id = entregadorId;
            Plano_Id = planoId;

            AddNotifications(new Contract<LocacaoCriarComand>()
                .Requires()
                .IsGreaterThan(Moto_Id, 0, "MotoId", "Moto não informado!")
                .IsGreaterThan(Plano_Id, 0, "PlanoId", "Plano não informado!")
                .IsGreaterThan(Entregador_Id, 0, "EntregadorId", "Entregador não informado!")
                .IsLowerOrEqualsThan(Data_Inicio, DateTime.Today, "DataInicio", "Data de início inválida!")
                .IsGreaterThan(Data_Previsao_Termino, Data_Inicio, "DataPrevisaoTermino", "Data de previsão de término deve ser após a data de início!")
            );
        }

        public int Moto_Id { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Previsao_Termino { get; set; }
        public int Entregador_Id { get; set; }
        public int Plano_Id { get; set; }

        public AlugarMotoCreated Map()
        {
            return new AlugarMotoCreated
            {
                MotoId = Moto_Id,
                EntregadorId = Entregador_Id,
                PlanoId = Plano_Id,
                DataInicio = Data_Inicio,
                DataPrevisaoTermino = Data_Previsao_Termino
            };
        }
    }
}
