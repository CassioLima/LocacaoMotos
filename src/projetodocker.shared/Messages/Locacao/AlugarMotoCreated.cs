using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class AlugarMotoCreated
    {
        public int MotoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }
        public int EntregadorId { get; set; }
        public int PlanoId { get; set; }
    }
}
