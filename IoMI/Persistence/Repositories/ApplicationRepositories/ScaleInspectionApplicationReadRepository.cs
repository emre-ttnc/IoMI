using IoMI.Application.Repositories.ApplicationRepositories;
using IoMI.Domain.Entities.ApplicationEntities;

namespace IoMI.Persistence.Repositories.ApplicationRepositories;

public class ScaleInspectionApplicationReadRepository : ReadRepository<ScaleInspectionApplication>, IScaleInspectionApplicationReadRepository
{
    public ScaleInspectionApplicationReadRepository(IoMIDbContext context) : base(context)
    {
    }
}
