using Application.Command;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IEntregadorService
    {
        void CreateEntregador(EntregadorCreated request);
        void SendFoto(EntregadorFotoCreated request);
    }
}
