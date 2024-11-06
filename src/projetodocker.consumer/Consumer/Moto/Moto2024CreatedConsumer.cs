using MassTransit;
using Microsoft.Extensions.Logging;
using Services;
using Shared.Messages;

namespace projetodocker.consumer.api.Consumer
{
    public class Moto2024CreatedConsumer : IConsumer<Moto2024Created>
    {
        private readonly ILogger<Moto2024Created> _logger;

        // Injeção de ILogger no construtor
        public Moto2024CreatedConsumer(ILogger<Moto2024Created> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Moto2024Created> motoCreated)
        {
            //Logar evento moto 2024
            _logger.LogInformation("Moto2024Created", motoCreated.Message);
        }
    }
}
