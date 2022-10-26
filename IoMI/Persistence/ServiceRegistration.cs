using IoMI.Application.Repositories.ApplicationRepositories;
using IoMI.Application.Repositories.InspectionRepositories;
using IoMI.Application.Repositories.InstrumentRepositories;
using IoMI.Application.Services;
using IoMI.Domain.Entities.UserEntities;
using IoMI.Persistence.Repositories.ApplicationRepositories;
using IoMI.Persistence.Repositories.InspectionRepositories;
using IoMI.Persistence.Repositories.InstrumentRepositories;
using IoMI.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IoMI.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<IoMIDbContext>(options => options.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=IoMIDb;"));

        services.AddIdentity<AppUser, UserRole>().AddEntityFrameworkStores<IoMIDbContext>().AddDefaultTokenProviders();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IInstrumentService, InstrumentService>();
        services.AddScoped<IApplicationService, ApplicationService>();

        services.AddScoped<IScaleReadRepository, ScaleReadRepository>();
        services.AddScoped<IScaleWriteRepository, ScaleWriteRepository>();
        services.AddScoped<IGasMeterReadRepository, GasMeterReadRepository>();
        services.AddScoped<IGasMeterWriteRepository, GasMeterWriteRepository>();

        services.AddScoped<IScaleInspectionReadRepository, ScaleInspectionReadRepository>();
        services.AddScoped<IScaleInspectionWriteRepository, ScaleInspectionWriteRepository>();
        services.AddScoped<IGasMeterInspectionReadRepository, GasMeterInspectionReadRepository>();
        services.AddScoped<IGasMeterInspectionWriteRepository, GasMeterInspectionWriteRepository>();

        services.AddScoped<IScaleInspectionApplicationReadRepository, ScaleInspectionApplicationReadRepository>();
        services.AddScoped<IScaleInspectionApplicationWriteRepository, ScaleInspectionApplicationWriteRepository>();
        services.AddScoped<IGasMeterInspectionApplicationReadRepository, GasMeterInspectionApplicationReadRepository>();
        services.AddScoped<IGasMeterInspectionApplicationWriteRepository, GasMeterInspectionApplicationWriteRepository>();
    }
}