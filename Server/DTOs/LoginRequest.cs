using System.ComponentModel.DataAnnotations;

namespace Server.DTOs
{
    public record LoginRequest(
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address")]
            string Email,
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(50, ErrorMessage = "Password cannot be longer than 50 characters")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long")]
            string Password
    );
}
