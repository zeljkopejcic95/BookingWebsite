using System.ComponentModel.DataAnnotations;

namespace Entities.Dto.User;

public class UserForUpdateDto
{
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
}
