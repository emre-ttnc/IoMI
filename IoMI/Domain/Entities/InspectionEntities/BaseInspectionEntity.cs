using IoMI.Domain.Entities.UserEntities;

namespace IoMI.Domain.Entities.InspectionEntities;

public class BaseInspectionEntity : BaseEntity
{
    public DateOnly? InspectionDate { get; set; }
    public bool IsValid { get; set; }

    public AppUser? Inspector { get; set; }
}