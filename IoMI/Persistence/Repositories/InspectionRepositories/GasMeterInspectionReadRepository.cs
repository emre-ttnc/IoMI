using IoMI.Application.Repositories.InspectionRepositories;
using IoMI.Domain.Entities.InspectionEntities;

namespace IoMI.Persistence.Repositories.InspectionRepositories;

public class GasMeterInspectionReadRepository : ReadRepository<GasMeterInspection>, IGasMeterInspectionReadRepository
{
    public GasMeterInspectionReadRepository(IoMIDbContext context) : base(context)
    {
    }
}