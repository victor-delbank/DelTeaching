using DelTeaching.Application.Config;
using DelTeaching.Application.Dtos.Mapping;
using DelTeaching.Application.Interfaces;
using DelTeaching.Application.Services;
using DelTeaching.Domain.IRepositories;
using DelTeaching.Infra.Data.Context;
using DelTeaching.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DelTeaching.Infra.IoC;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE") ?? configuration.GetConnectionString("Postgres");
        service.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        service.AddAutoMapper(typeof(MappingProfile));

        service.AddScoped<IUnitOfWork, UnitOfWork>();

        MessageBusConfig rabbitConfig = configuration.GetSection("RabbitMQ").Exists()
                                        ? configuration.GetSection("RabbitMQ").Get<MessageBusConfig>()
                                        : new MessageBusConfig();

        service.AddSingleton(rabbitConfig);

        service.AddScoped<IMessageBus, MessageBus>();

        //service.AddScoped<ITService, TService>();

        return service;
    }
}