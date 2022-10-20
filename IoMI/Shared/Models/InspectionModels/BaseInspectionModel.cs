using IoMI.Shared.Models.UserModels;

namespace IoMI.Shared.Models.InspectionModels;

public class BaseInspectionModel
{
    public Guid Id { get; set; }
    public DateOnly? InspectionDate { get; set; }
    public bool IsValid { get; set; }

    public UserModel? Inspector { get; set; }
}