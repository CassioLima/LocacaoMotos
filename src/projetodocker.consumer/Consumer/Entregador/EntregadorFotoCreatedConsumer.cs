using MassTransit;
using Services;
using Shared.Messages;

namespace projetodocker.consumer.api.Consumer
{
    public class EntregadorFotoCreatedConsumer : IConsumer<EntregadorFotoCreated>
    {
        private IEntregadorService _entregadorService;
        public EntregadorFotoCreatedConsumer(IEntregadorService _EntregadorService)
        {
            this._entregadorService = _EntregadorService;
        }

        public async Task Consume(ConsumeContext<EntregadorFotoCreated> motoCreated)
        {
            _entregadorService.SendFoto(motoCreated.Message);
        }
    }
}
