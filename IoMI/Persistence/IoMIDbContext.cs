using IoMI.Domain.Entities.ApplicationEntities;
using IoMI.Domain.Entities.InspectionEntities;
using IoMI.Domain.Entities.InstrumentEntities;
using IoMI.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IoMI.Persistence;

public class IoMIDbContext : IdentityDbContext<AppUser, UserRole, string>
{
    public IoMIDbContext(DbContextOptions options) : base(options) { }

    public DbSet<GasMeter>? GasMeters { get; set; }
    public DbSet<Scale>? Scales { get; set; }
    public DbSet<GasMeterInspection>? GasMeterInspections { get; set; }
    public DbSet<ScaleInspection>? ScaleInspections { get; set; }
    public DbSet<GasMeterInspectionApplication>? GasMeterInspectionApplications { get; set; }
    public DbSet<ScaleInspectionApplication>? ScaleInspectionApplications { get; set; }

}