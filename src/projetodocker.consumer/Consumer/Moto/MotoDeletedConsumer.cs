using MassTransit;
using Services;
using Shared.Messages;

namespace projetodocker.consumer.api.Consumer
{
    public class MotoDeletedConsumer : IConsumer<MotoDeleted>
    {

        private IMotoService _motoService;
        public MotoDeletedConsumer(IMotoService _motoService)
        {
            this._motoService = _motoService;
        }

        public async Task Consume(ConsumeContext<MotoDeleted> motoDeleted)
        {
            _motoService.DeleteMoto(motoDeleted.Message);
        }
    }
}
