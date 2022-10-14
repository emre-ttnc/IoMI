using System.ComponentModel.DataAnnotations;

namespace IoMI.Shared.Models.UserModels;

public class SendConfirmEmailModel
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid format. Please check the email address.")]
    public string? Email { get; set; }
}