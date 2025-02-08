using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.CreateUser;

public class CreateUserRequest : BaseRequest
{
    [Required(ErrorMessage = "Username is missing")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "PasswordHash is missing")]
    public required string PasswordHash { get; set; }

    [Required(ErrorMessage = "Email is missing")]
    public required string Email { get; set; }
}