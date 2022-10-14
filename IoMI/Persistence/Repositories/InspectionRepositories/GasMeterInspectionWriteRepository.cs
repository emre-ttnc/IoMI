using IoMI.Application.Repositories.InspectionRepositories;
using IoMI.Domain.Entities.InspectionEntities;

namespace IoMI.Persistence.Repositories.InspectionRepositories;

public class GasMeterInspectionWriteRepository : WriteRepository<GasMeterInspection>, IGasMeterInspectionWriteRepository
{
    public GasMeterInspectionWriteRepository(IoMIDbContext context) : base(context)
    {
    }
}