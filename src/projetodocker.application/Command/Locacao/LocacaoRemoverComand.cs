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
    public class LocacaoRemoverComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public LocacaoRemoverComand(int id, int usuarioId)
        {
            Id = id;
            UsuarioId = usuarioId;

            AddNotifications(new Contract<LocacaoRemoverComand>()
                .Requires()
                .IsGreaterThan(Id, 0, "Id", "Id da locação não informado!")
                .IsGreaterThan(UsuarioId, 0, "UsuarioId", "Usuário não informado!")
            );
        }

        public int Id { get; set; }
        public int UsuarioId { get; set; }
    }
}