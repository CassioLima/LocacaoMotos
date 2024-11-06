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
    public class MotoCriarComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public MotoCriarComand(string placa, string marca, string modelo, int ano)
        {
            Placa = placa;
            Marca = marca;
            Modelo = modelo;
            Ano = ano;
            Guid = Guid.NewGuid();

            AddNotifications(new Contract<MotoCriarComand>()
                .Requires()
                .IsNotNullOrWhiteSpace(Placa, "Placa", "Placa não informada!")
                .IsNotNullOrWhiteSpace(Marca, "Marca", "Marca não informada!")
                .IsNotNullOrWhiteSpace(Modelo, "Modelo", "Modelo não informado!")
                .IsGreaterThan(Ano, 1900, "Ano", "Ano inválido!")
            );
        }

        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public Guid Guid { get; set; }

        public MotoCreated Map()
        {
            return new MotoCreated
            {
                Placa = Placa,
                Marca = Marca,
                Modelo = Modelo,
                Ano = Ano,
                Guid = Guid
            };
        }

        public Moto2024Created Map2024()
        {
            return new Moto2024Created
            {
                Placa = Placa,
                Marca = Marca,
                Modelo = Modelo,
                Ano = Ano,
                Guid = Guid
            };
        }
    }
}
