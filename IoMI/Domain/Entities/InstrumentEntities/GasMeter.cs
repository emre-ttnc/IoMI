using IoMI.Domain.Entities.ApplicationEntities;
using IoMI.Domain.Entities.InspectionEntities;

namespace IoMI.Domain.Entities.InstrumentEntities;

public class GasMeter : BaseInstrumentEntity
{
    public string? MaxFlowRate { get; set; }

    public ICollection<GasMeterInspection> Inspections { get; set; } = new HashSet<GasMeterInspection>();
    public ICollection<GasMeterInspectionApplication> Applications { get; set; } = new HashSet<GasMeterInspectionApplication>();
}