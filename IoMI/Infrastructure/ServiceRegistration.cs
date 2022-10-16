using IoMI.Application.Abstractions.AuthToken;
using IoMI.Application.Services;
using IoMI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IoMI.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ITokenHandler, TokenHandler>();
    }
}