using IoMI.Domain.Entities.InstrumentEntities;

namespace IoMI.Domain.Entities.ApplicationEntities;

public class ScaleInspectionApplication : BaseApplicationEntity
{
    public ICollection<Scale> Scales { get; set; } = new HashSet<Scale>();
}
