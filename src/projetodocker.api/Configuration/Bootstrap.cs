using Application;
using Domain;
using Infra;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

namespace API
{
    public static class Bootstrap
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager config)
        {
            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(ICommandResult).Assembly); });

            services.AddScoped<INotificationContext, NotificationContext>();

            services.AddDbContext<DbContext2>(options => options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork>(provider => provider.GetService<DbContext2>());

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            return services;
        }

        //public static IHostBuilder ConfigureLog(this IHostBuilder hostBuilder)
        //{
        //    hostBuilder.UseSerilog((context, configuration) =>
        //    {
        //        configuration
        //        .WriteTo
        //        .Elasticsearch(new[] { new Uri("http://elasticsearch:9200") }, opts =>
        //        {
        //            opts.DataStream = new DataStreamName("logs-api", "example", "demo");
        //            opts.BootstrapMethod = BootstrapMethod.Failure;
        //        })
        //        .ReadFrom
        //        .Configuration(context.Configuration);
        //    });
        //    return hostBuilder;
        //}
        //public static IApplicationBuilder ConfigureHealthCheck(this IApplicationBuilder builder)
        //{
        //    builder.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
        //    {
        //        Predicate = p => true,
        //        ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse
        //    });
        //    return builder;
        //}

    }
}
