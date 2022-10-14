using IoMI.Application.Repositories.InstrumentRepositories;
using IoMI.Domain.Entities.InstrumentEntities;

namespace IoMI.Persistence.Repositories.InstrumentRepositories;

public class ScaleReadRepository : ReadRepository<Scale>, IScaleReadRepository
{
    public ScaleReadRepository(IoMIDbContext context) : base(context)
    {
    }
}