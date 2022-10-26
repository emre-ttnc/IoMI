using IoMI.Application.Repositories.ApplicationRepositories;
using IoMI.Domain.Entities.ApplicationEntities;

namespace IoMI.Persistence.Repositories.ApplicationRepositories;

public class GasMeterInspectionApplicationWriteRepository : WriteRepository<GasMeterInspectionApplication>, IGasMeterInspectionApplicationWriteRepository
{
    public GasMeterInspectionApplicationWriteRepository(IoMIDbContext context) : base(context)
    {
    }
}
