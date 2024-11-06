using MassTransit;
using Services;
using Shared.Messages;

namespace projetodocker.consumer.api.Consumer
{
    public class MotoUpdatedConsumer : IConsumer<MotoUpdated>
    {
        private IMotoService _motoService;
        public MotoUpdatedConsumer(IMotoService _motoService)
        {
            this._motoService = _motoService;
        }

        public async Task Consume(ConsumeContext<MotoUpdated> motoCreated)
        {
            _motoService.UpdateMoto(motoCreated.Message);
        }
    }
}
