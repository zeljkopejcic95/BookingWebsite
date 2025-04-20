using System.ComponentModel.DataAnnotations;
using Entities.Enums;

namespace Entities.Dto.User;

public class UserDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [MaxLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [MaxLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [MaxLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    [MaxLength(20, ErrorMessage = "Role cannot be longer than 20 characters.")]
    public UserRole Role { get; set; }
}
