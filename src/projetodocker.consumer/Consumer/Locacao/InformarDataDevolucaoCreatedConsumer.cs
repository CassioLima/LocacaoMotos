using MassTransit;
using Services;
using Shared.Messages;

namespace projetodocker.consumer.api.Consumer
{
    public class InformarDataDevolucaoCreatedConsumer : IConsumer<InformarDataDevolucaoCreated>
    {
        private ILocacaoService _locacaoService;
        public InformarDataDevolucaoCreatedConsumer(ILocacaoService locacaoService)
        {
            this._locacaoService = locacaoService;
        }

        public async Task Consume(ConsumeContext<InformarDataDevolucaoCreated> motoCreated)
        {
            _locacaoService.InformarDevolucaoCalcularValor(motoCreated.Message);
        }
    }
}
