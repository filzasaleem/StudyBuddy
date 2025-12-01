using System.ComponentModel.DataAnnotations;

namespace Server.DTOs;

public record UserRequest(
    [Required(ErrorMessage = "First name is required")] [MaxLength(50)] string FirstName,
    [Required(ErrorMessage = "Last name is required")] [MaxLength(50)] string LastName,
    [Required]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address")]
        string Email,
    [Required]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and include uppercase, lowercase, number, and special character"
    )]
        string PasswordHash
);
