using Application.Command;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ILocacaoService
    {
        void AlugarMoto(AlugarMotoCreated request);
        void InformarDevolucaoCalcularValor(InformarDataDevolucaoCreated request);
    }
}
