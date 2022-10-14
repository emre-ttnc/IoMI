using IoMI.Domain.Entities.InspectionEntities;

namespace IoMI.Domain.Entities.InstrumentEntities;

public class Scale : BaseInstrumentEntity
{
    public string? MaximumCapacity { get; set; }
    public string? AccuracyClass { get; set; }

    public ICollection<ScaleInspection> Inspections { get; set; } = new HashSet<ScaleInspection>();
}