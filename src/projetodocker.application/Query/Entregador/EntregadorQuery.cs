using MediatR;
using Domain;
using Domain.Entity;
using Enums;

namespace Application
{
    public class EntregadorQuery : IRequest<CommandResult>
    {
        public EntregadorQuery()
        {

        }
    }
    public class EntregadorResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Numero_CNH { get; set; }
        public string Tipo_CNH { get; set; }
        public string Imagem_CNH { get; set; }
        public DateTime Data_Nascimento { get; set; }

        public static EntregadorResult Map(Entregador tarefa)
        {
            EntregadorResult result = new();
            result.Id = tarefa.Id;
            result.Nome = tarefa.Nome;
            result.CNPJ = tarefa.CNPJ;
            result.Tipo_CNH = tarefa.TipoCNH;
            result.Imagem_CNH = tarefa.ImagemCNH;
            result.Data_Nascimento = tarefa.DataNascimento;
            return result;
        }

    }
}