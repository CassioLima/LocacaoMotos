using MediatR;
using Domain;
using Domain.Entity;
using Enums;

namespace Application
{
    public class LocacaoQuery : IRequest<CommandResult>
    {
        public LocacaoQuery()
        {

        }
    }
    public class LocacaoResult
    {
        public int Id { get; set; }
        public int MotoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }
        public DateTime? DataTermino { get; set; }
        public int EntregadorId { get; set; }
        public int PlanoId { get; set; }

        public static LocacaoResult Map(Locacao locacao)
        {
            LocacaoResult result = new();
            result.Id = result.Id;
            result.MotoId = locacao.MotoId;
            result.EntregadorId = locacao.EntregadorId;
            result.PlanoId = locacao.PlanoId;
            result.DataCriacao = locacao.DataCriacao;
            result.DataInicio = locacao.DataInicio;
            result.DataPrevisaoTermino = locacao.DataPrevisaoTermino;
            result.DataTermino = locacao.DataTermino;
            return result;
        }

    }
}