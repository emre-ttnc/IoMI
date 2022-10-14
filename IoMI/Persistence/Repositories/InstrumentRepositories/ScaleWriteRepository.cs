using IoMI.Application.Repositories.InstrumentRepositories;
using IoMI.Domain.Entities.InstrumentEntities;

namespace IoMI.Persistence.Repositories.InstrumentRepositories;

public class ScaleWriteRepository : WriteRepository<Scale>, IScaleWriteRepository
{
    public ScaleWriteRepository(IoMIDbContext context) : base(context)
    {
    }
}