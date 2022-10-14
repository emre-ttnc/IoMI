using IoMI.Application.Repositories.InstrumentRepositories;
using IoMI.Domain.Entities.InstrumentEntities;

namespace IoMI.Persistence.Repositories.InstrumentRepositories;

public class GasMeterWriteRepository : WriteRepository<GasMeter>, IGasMeterWriteRepository
{
    public GasMeterWriteRepository(IoMIDbContext context) : base(context)
    {
    }
}