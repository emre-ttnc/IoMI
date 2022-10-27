using IoMI.Application.Repositories.ApplicationRepositories;
using IoMI.Domain.Entities.ApplicationEntities;

namespace IoMI.Persistence.Repositories.ApplicationRepositories;

public class ScaleInspectionApplicationWriteRepository : WriteRepository<ScaleInspectionApplication>, IScaleInspectionApplicationWriteRepository
{
    public ScaleInspectionApplicationWriteRepository(IoMIDbContext context) : base(context)
    {
    }
}
