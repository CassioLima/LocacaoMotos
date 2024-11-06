using MassTransit;
using Services;
using Shared.Messages;

namespace projetodocker.consumer.api.Consumer
{
    public class MotoCreatedConsumer : IConsumer<MotoCreated>
    {
        private IMotoService _motoService;
        public MotoCreatedConsumer(IMotoService _motoService)
        {
            this._motoService = _motoService;
        }

        public async Task Consume(ConsumeContext<MotoCreated> motoCreated)
        {
            _motoService.CreateMoto(motoCreated.Message);
        }
    }
}
