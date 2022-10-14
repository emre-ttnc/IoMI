using IoMI.Application.Repositories.InspectionRepositories;
using IoMI.Domain.Entities.InspectionEntities;

namespace IoMI.Persistence.Repositories.InspectionRepositories;

public class ScaleInspectionWriteRepository : WriteRepository<ScaleInspection>, IScaleInspectionWriteRepository
{
    public ScaleInspectionWriteRepository(IoMIDbContext context) : base(context)
    {
    }
}