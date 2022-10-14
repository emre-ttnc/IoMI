using IoMI.Domain.Entities.UserEntities;

namespace IoMI.Domain.Entities.InstrumentEntities;

public class BaseInstrumentEntity : BaseEntity
{
    public string? Brand { get; set; }
    public string? TypeOrModel { get; set; }
    public string? SerialNumber { get; set; }
    public int LastInspectionYear { get; set; }
    public bool IsActive { get; set; }

    public AppUser? UserOfInstrument { get; set; }
}