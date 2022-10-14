using Microsoft.AspNetCore.Identity;

namespace IoMI.Domain.Entities.UserEntities;

public class AppUser : IdentityUser<string>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? RegistryCode { get; set; }
    public bool IsActive { get; set; }
    public string? Address { get; set; }
    public string? CompanyName { get; set; }
}