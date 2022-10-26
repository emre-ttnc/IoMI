using IoMI.Domain.Entities.UserEntities;

namespace IoMI.Domain.Entities.ApplicationEntities;

public class BaseApplicationEntity : BaseEntity
{
    public DateOnly ApplicationDate { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsCompleted { get; set; }
    public AppUser? Applicant { get; set; }
}
