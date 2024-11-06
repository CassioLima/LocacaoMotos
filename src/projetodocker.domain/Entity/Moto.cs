using Shared.Messages;

namespace Domain.Entity
{
    public class Moto : EntityBase
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }     
        
        public MotoCreated Map()
        {
            return new MotoCreated
            {
                Placa = Placa,
                Marca = Marca,
                Modelo = Modelo,
                Ano = Ano
            };
        }
    }
}

