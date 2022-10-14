using IoMI.Domain.Entities.InstrumentEntities;

namespace IoMI.Domain.Entities.InspectionEntities;

public class ScaleInspection : BaseInspectionEntity
{
    public ICollection<Scale> Scales { get; set; } = new HashSet<Scale>();
}