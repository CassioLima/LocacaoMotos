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

namespace Services
{
    public class MotoService: IMotoService
    {

        private readonly IRepositoryBase<Moto> repository;
        private readonly INotificationContext _notificationContext;

        public MotoService(IRepositoryBase<Moto> repository, INotificationContext notificationContext)
        {
            this.repository = repository;
            _notificationContext = notificationContext;
        }

        public void CreateMoto(MotoCreated motoCreated)
        {

            MotoCriarComand request = new MotoCriarComand(motoCreated.Placa, motoCreated.Marca, motoCreated.Modelo, motoCreated.Ano);

            if (!request.IsValid)
            {
                _notificationContext.AddNotification(request.Notifications);
                return;
            }

            Moto moto = new Moto
            {
                Placa = request.Placa,
                Marca = request.Marca,
                Modelo = request.Modelo,
                Ano = request.Ano
            };
            var success = this.repository.Save(moto);
        }

        public void DeleteMoto(MotoDeleted request)
        {
            this.repository.Delete(request.Id);
        }

        public void UpdateMoto(MotoUpdated motoUpdated)
        {
            MotoAtualizarComand request = new MotoAtualizarComand(motoUpdated.Id, motoUpdated.Placa);

            if (!request.IsValid)
            {
                _notificationContext.AddNotification(request.Notifications);
                return;
            }

            Moto moto = this.repository.GetById(request.Id);
            moto.Placa = request.Placa;
            var success = repository.Update(moto);
        }
    }
}
