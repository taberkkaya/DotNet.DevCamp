using System.ComponentModel.DataAnnotations;

namespace ProductAPI.DTO;

public class UserDTO
{
    [Required]
    public string FullName { get; set; } = null!;
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; } = null!;
}