namespace IoMI.Shared.Models.UserModels;

public class ConfirmEmailModel
{
    public Guid UserId { get; set; }
    public string? Token { get; set; }
}