using IoMI.Application.Repositories.InspectionRepositories;
using IoMI.Domain.Entities.InspectionEntities;

namespace IoMI.Persistence.Repositories.InspectionRepositories;

public class ScaleInspectionReadRepository : ReadRepository<ScaleInspection>, IScaleInspectionReadRepository
{
    public ScaleInspectionReadRepository(IoMIDbContext context) : base(context)
    {
    }
}