using Application;
using Domain;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Infra;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using projetodocker.consumer.api.Consumer;
using Serilog;
using Services;
using System.Reflection;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddScoped<IUnitOfWork>(provider => provider.GetService<DbContext2>());
    services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
    services.AddScoped<IMotoService, MotoService>();
    services.AddScoped<IEntregadorService, EntregadorService>();
    services.AddScoped<ILocacaoService, LocacaoService>();
    services.AddScoped<INotificationContext, NotificationContext>();
    
    services.AddLogging(loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddSerilog();
    });

    services.AddTransient<Moto2024CreatedConsumer>();

    services.AddDbContext<DbContext2>(options =>options.UseNpgsql(hostContext.Configuration.GetConnectionString("DefaultConnection")));

    services.AddMassTransit(busConfigurator =>
    {
        var entryAssembly = Assembly.GetExecutingAssembly();

        busConfigurator.AddConsumers(entryAssembly);
        busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
        {
            busFactoryConfigurator.Host("rabbitmq", "/", h => {});

            busFactoryConfigurator.ConfigureEndpoints(context);
        });
    });
});

Log.Logger = new LoggerConfiguration()
             .WriteTo
             .Elasticsearch(new[] { new Uri("http://elasticsearch:9200") }, opts =>
             {
                 opts.DataStream = new DataStreamName("logs-api", "example", "demo");
                 opts.BootstrapMethod = BootstrapMethod.Failure;
             })
             .CreateLogger();

var app = builder.Build();

app.Run();