using IoMI.Domain.Entities.InstrumentEntities;

namespace IoMI.Domain.Entities.InspectionEntities;

public class GasMeterInspection : BaseInspectionEntity
{
    public ICollection<GasMeter> GasMeter { get; set; } = new HashSet<GasMeter>();
}