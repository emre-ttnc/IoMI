namespace IoMI.Shared.Models.UserModels;

public class VerifyResetTokenModel
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
}