using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class EntregadorFotoCreated
    {
        public int Id { get; set; }
        public string ImagemCNH { get; set; } = string.Empty;
    }
}
