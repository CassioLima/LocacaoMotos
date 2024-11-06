using API;
using Infra;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Ingest.Elasticsearch;
using Elastic.Serilog.Sinks;
using Elastic.CommonSchema;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
    .WriteTo
    .Elasticsearch(new[] { new Uri("http://elasticsearch:9200") }, opts =>
    {
        opts.DataStream = new DataStreamName("logs-api", "example", "demo");
        opts.BootstrapMethod = BootstrapMethod.Failure;
    })
    .ReadFrom
    .Configuration(context.Configuration);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        busFactoryConfigurator.Host("rabbitmq", hostConfigurator => { });
    });
});

var app = builder.Build();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DbContext2>();
    context.Database.EnsureCreated();
    context.SeedData();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    await next.Invoke();
    stopwatch.Stop();

   Serilog.Log.Information("Método: {Method}, Path: {Path}, Body: {Body} Status Code: {StatusCode}, Tempo: {ElapsedMilliseconds}ms",
        context.Request.Method,
        context.Request.Path,
        context.Request.Body,
        context.Response.StatusCode,
        stopwatch.ElapsedMilliseconds);
});

app.Run();
