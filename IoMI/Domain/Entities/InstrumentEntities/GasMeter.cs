using IoMI.Domain.Entities.InspectionEntities;

namespace IoMI.Domain.Entities.InstrumentEntities;

public class GasMeter : BaseInstrumentEntity
{
    public string? MaxFlowRate { get; set; }

    public ICollection<GasMeterInspection> Inspections { get; set; } = new HashSet<GasMeterInspection>();
}