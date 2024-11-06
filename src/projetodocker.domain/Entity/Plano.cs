using Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Plano : EntityBase
    {
        public string Nome { get; set; } = string.Empty;
        public int QuantidadeDias { get; set; }
        public decimal Valor { get; set; }
    }
}
