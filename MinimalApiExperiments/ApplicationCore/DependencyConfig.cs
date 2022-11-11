using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiExperiments.ApplicationCore.Common.Behaviours;
using MinimalApiExperiments.ApplicationCore.Infrastructure.Persistence;
using System.Reflection;

namespace MinimalApiExperiments.ApplicationCore;
public static class DependencyConfig
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddSqlServer<AppDbContext>(config.GetConnectionString("DefaultConnection"));

        return services;
    }
}
