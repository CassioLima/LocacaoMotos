using Application.Command;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IMotoService
    {
        void CreateMoto(MotoCreated request);
        void UpdateMoto(MotoUpdated request);
        void DeleteMoto(MotoDeleted request);
    }
}
