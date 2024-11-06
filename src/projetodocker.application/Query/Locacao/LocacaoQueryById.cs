using MediatR;
using Domain;
using Domain.Entity;
using Enums;

namespace Application
{
    public class LocacaoQueryById : IRequest<CommandResult>
    {
        public LocacaoQueryById(int Id)
        {
            this.Id = Id;
        }
        public int Id { get; set; }
    }
    public class LocacaoByIDResult
    {
        public int Id { get; set; }
        public int MotoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }
        public DateTime? DataTermino { get; set; }
        public int EntregadorId { get; set; }
        public int PlanoId { get; set; }

        public static LocacaoByIDResult Map(Locacao entity)
        {
            LocacaoByIDResult result = new();
            result.Id = entity.Id;
            result.MotoId = entity.MotoId;
            result.EntregadorId = entity.EntregadorId;
            result.MotoId = entity.MotoId;
            result.DataCriacao = entity.DataCriacao;
            result.DataInicio = entity.DataInicio;
            result.DataPrevisaoTermino = entity.DataPrevisaoTermino;
            result.DataTermino = entity.DataTermino;
            return result;
        }

    }
}

