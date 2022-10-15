using System.ComponentModel.DataAnnotations;

namespace IoMI.Shared.Models.UserModels;

public class UserRegisterModel
{
    public Guid? Id { get; set; }

    [Required]
    [StringLength(16, ErrorMessage = "Name can be up to 16 and at least 3 characters.", MinimumLength = 3)]
    [RegularExpression(@"^[a-zA-Z\s.\-']{3,16}$", ErrorMessage = "Name contains invalid characters.")]
    public string? Name { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z\s.\-']{3,16}$", ErrorMessage = "Surname contains invalid characters.")]
    [StringLength(16, ErrorMessage = "Surname can be up to 16 and at least 3 characters.", MinimumLength = 3)]
    public string? Surname { get; set; }

    [Required]
    [StringLength(16, ErrorMessage = "Username can be up to 16 and at least 6 characters.", MinimumLength = 6)]
    [RegularExpression(@"^[a-z]+$", ErrorMessage = "Username must be in English lowercase letters only.")]
    public string? Username { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid format. Please check the email address.")]
    public string? Email { get; set; }

    [Required]
    [StringLength(16, ErrorMessage = "Password can be up to 16 and at least 6 characters.", MinimumLength = 6)]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).\S{6,16}$", ErrorMessage = "Password must contain atleast 1 uppercase, 1 lowercase, 1 special character and a number. No spaces.")]
    public string? Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "Password does not match.")]
    public string? ConfirmPassword { get; set; }

    public bool IsActive { get; set; }

    public string? Address { get; set; }

    public string? CompanyName { get; set; }
}