using IoMI.Domain.Entities.InstrumentEntities;

namespace IoMI.Domain.Entities.ApplicationEntities;

public class GasMeterInspectionApplication : BaseApplicationEntity
{
    public ICollection<GasMeter> GasMeters { get; set; } = new HashSet<GasMeter>();
}
