using MassTransit;
using Services;
using Shared.Messages;

namespace projetodocker.consumer.api.Consumer
{
    public class AlugarMotoCreatedConsumer : IConsumer<AlugarMotoCreated>
    {
        private ILocacaoService _locacaoService;
        public AlugarMotoCreatedConsumer(ILocacaoService locacaoService)
        {
            this._locacaoService = locacaoService;
        }

        public async Task Consume(ConsumeContext<AlugarMotoCreated> alugarCreated)
        {
            _locacaoService.AlugarMoto(alugarCreated.Message);
        }
    }
}
