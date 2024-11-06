using Application;
using Domain.Entity;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Application.Command;
using Shared.Messages;
using images;

namespace Services
{
    public class EntregadorService : IEntregadorService
    {

        private readonly IRepositoryBase<Entregador> repository;
        private readonly INotificationContext _notificationContext;

        public EntregadorService(IRepositoryBase<Entregador> repository, INotificationContext notificationContext)
        {
            this.repository = repository;
            _notificationContext = notificationContext;
        }

        public void CreateEntregador(EntregadorCreated entregadorCreated)
        {

            EntregadorCriarComand request = new EntregadorCriarComand(entregadorCreated.Nome, entregadorCreated.CNPJ, entregadorCreated.DataNascimento, entregadorCreated.NumeroCNH, entregadorCreated.TipoCNH, entregadorCreated.ImagemCNH);

            if (!request.IsValid)
            {
                _notificationContext.AddNotification(request.Notifications);
                return;
            }

            Entregador entregador = new Entregador
            {
                Nome = request.Nome,
                CNPJ = request.CNPJ,
                DataNascimento = request.Data_Nascimento,
                TipoCNH = request.Tipo_CNH,
                NumeroCNH = request.Numero_CNH,
                ImagemCNH = request.Imagem_CNH
            };

            this.repository.Save(entregador);
    }

        public void SendFoto(EntregadorFotoCreated entregadorFotoCreated)
        {
            EntregadorFotoCriarComand request = new EntregadorFotoCriarComand(entregadorFotoCreated.Id, entregadorFotoCreated.ImagemCNH);

            if (!request.IsValid)
            {
                _notificationContext.AddNotification(request.Notifications);
                return;
            }

            Entregador entregador = this.repository.GetById(request.Id);

            var imageSaver = new ImageSaver();
            string base64Image = request.Imagem_CNH;
            bool success = imageSaver.SaveImageFromBase64(base64Image, entregador.Id.ToString());


            entregador.ImagemCNH = request.Imagem_CNH;
            this.repository.Update(entregador);

        }
    }
}
