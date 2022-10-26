using IoMI.Application.Repositories.ApplicationRepositories;
using IoMI.Domain.Entities.ApplicationEntities;

namespace IoMI.Persistence.Repositories.ApplicationRepositories;

public class GasMeterInspectionApplicationReadRepository : ReadRepository<GasMeterInspectionApplication>, IGasMeterInspectionApplicationReadRepository
{
    public GasMeterInspectionApplicationReadRepository(IoMIDbContext context) : base(context)
    {
    }
}
