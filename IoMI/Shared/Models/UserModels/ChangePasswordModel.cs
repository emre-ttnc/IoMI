using System.ComponentModel.DataAnnotations;

namespace IoMI.Shared.Models.UserModels;

public class ChangePasswordModel
{
    [Required]
    [StringLength(16, ErrorMessage = "Password can be up to 16 and at least 6 characters.", MinimumLength = 6)]
    public string? OldPassword { get; set; }

    [Required]
    [StringLength(16, ErrorMessage = "Password can be up to 16 and at least 6 characters.", MinimumLength = 6)]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).\S{6,16}$", ErrorMessage = "Password must contain atleast 1 uppercase, 1 lowercase, 1 special character and a number. No spaces.")]
    public string? Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "Password does not match.")]
    public string? ConfirmPassword { get; set; }
}