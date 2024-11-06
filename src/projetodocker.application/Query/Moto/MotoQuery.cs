using MediatR;
using Domain;
using Domain.Entity;
using Enums;

namespace Application
{
    public class MotoQuery : IRequest<CommandResult>
    {
        public MotoQuery()
        {
        }
    }
    public class MotoResult
    {
        public int Identificador { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }

        public static MotoResult Map(Moto entity)
        {
            if (entity == null) return null;

            MotoResult result = new();
            result.Identificador = entity.Id;
            result.Placa = entity.Placa;
            result.Modelo = entity.Modelo;
            result.Ano = entity.Ano;
            return result;
        }

    }
}

