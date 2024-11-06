using MediatR;
using Domain;
using Domain.Entity;
using Enums;

namespace Application
{
    public class MotoQueryById : IRequest<CommandResult>
    {
        public MotoQueryById(int Id)
        {
            this.Id = Id;
        }
        public int Id { get; set; }
    }
    public class MotoByIDResult
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }

        public static MotoByIDResult Map(Moto entity)
        {
            MotoByIDResult result = new();
            result.Id = entity.Id;
            result.Placa = entity.Placa;
            result.Marca = entity.Marca;
            result.Modelo = entity.Modelo;
            result.Ano = entity.Ano;
            return result;
        }

    }
}

