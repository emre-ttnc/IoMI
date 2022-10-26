using IoMI.Application.Repositories.ApplicationRepositories;
using IoMI.Domain.Entities.ApplicationEntities;
using IoMI.Persistence.Repositories;

namespace IoMI.Persistence.Services;

public class ScaleInspectionApplicationWriteRepository : WriteRepository<ScaleInspectionApplication>, IScaleInspectionApplicationWriteRepository
{
    public ScaleInspectionApplicationWriteRepository(IoMIDbContext context) : base(context)
    {
    }
}
