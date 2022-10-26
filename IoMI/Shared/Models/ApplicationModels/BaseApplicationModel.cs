using IoMI.Shared.Models.UserModels;

namespace IoMI.Shared.Models.ApplicationModels;

public abstract class BaseApplicationModel
{
    public Guid Id { get; set; }
    public DateOnly ApplicationDate { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsCompleted { get; set; }
    public UserModel? Applicant { get; set; }
}
