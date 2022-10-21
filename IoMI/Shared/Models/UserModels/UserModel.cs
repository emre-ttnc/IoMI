namespace IoMI.Shared.Models.UserModels;

public class UserModel
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Address { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string? RegistryCode { get; set; }
    public string? Role { get; set; }
}
