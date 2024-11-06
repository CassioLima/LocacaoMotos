using MassTransit;
using Services;
using Shared.Messages;

namespace projetodocker.consumer.api.Consumer
{
    public class EntregadorCreatedConsumer : IConsumer<EntregadorCreated>
    {
        private IEntregadorService _entergadorService;
        public EntregadorCreatedConsumer(IEntregadorService _entregadorService)
        {
            this._entergadorService = _entregadorService;
        }

        public async Task Consume(ConsumeContext<EntregadorCreated> entregadorCreated)
        {
            _entergadorService.CreateEntregador(entregadorCreated.Message);
        }
    }
}
