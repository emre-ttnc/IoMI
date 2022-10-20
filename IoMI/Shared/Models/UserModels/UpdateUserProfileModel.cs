using System.ComponentModel.DataAnnotations;

namespace IoMI.Shared.Models.UserModels;

public class UpdateUserProfileModel
{
    public string? Id { get; set; }

    [Required]
    [StringLength(16, ErrorMessage = "Name can be up to 16 and at least 3 characters.", MinimumLength = 3)]
    [RegularExpression(@"^[a-zA-Z\s.\-']{3,16}$", ErrorMessage = "Name contains invalid characters.")]
    public string? Name { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z\s.\-']{3,16}$", ErrorMessage = "Surname contains invalid characters.")]
    [StringLength(16, ErrorMessage = "Surname can be up to 16 and at least 3 characters.", MinimumLength = 3)]
    public string? Surname { get; set; }

    [Required]
    [StringLength(255, ErrorMessage = "Company name can be up to 255 and at least 3 characters.", MinimumLength = 3)]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    [StringLength(255, ErrorMessage = "Address can be up to 255 and at least 6 characters.", MinimumLength = 6)]
    public string Address { get; set; } = string.Empty;
}