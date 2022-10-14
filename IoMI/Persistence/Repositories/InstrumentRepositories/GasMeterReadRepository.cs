using IoMI.Application.Repositories.InstrumentRepositories;
using IoMI.Domain.Entities.InstrumentEntities;

namespace IoMI.Persistence.Repositories.InstrumentRepositories;

public class GasMeterReadRepository : ReadRepository<GasMeter>, IGasMeterReadRepository
{
    public GasMeterReadRepository(IoMIDbContext context) : base(context)
    {
    }
}