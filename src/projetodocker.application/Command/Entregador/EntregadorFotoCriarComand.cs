using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Shared.Messages;


namespace Application.Command
{
    public class EntregadorFotoCriarComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public EntregadorFotoCriarComand(int id, string imagemCNH)
        {
            Id = id;
            Imagem_CNH = imagemCNH;

            AddNotifications(new Contract<EntregadorCriarComand>()
                .Requires()
                .IsNotNullOrWhiteSpace(Imagem_CNH, "ImagemCNH", "Imagem CNH não informado!")
            );
        }

        public int Id { get; set; }
        public string Imagem_CNH { get; set; }

        public EntregadorFotoCreated Map()
        {
            return new EntregadorFotoCreated
            {
                ImagemCNH = Imagem_CNH,
                Id = Id
            };
        }

    }
}
