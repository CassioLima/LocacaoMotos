namespace Domain.Entity
{
    public class Locacao : EntityBase
    {
        public int MotoId { get; set; }
        public int EntregadorId { get; set; }
        public int PlanoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }
        public DateTime? DataTermino { get; set; }
        public Entregador Entregador { get; set; }
        public Plano Plano { get; set; }
        public Moto Moto { get; set; }
    }
}
