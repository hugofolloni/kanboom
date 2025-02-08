using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models;

public class UserRequest : BaseRequest
{
    [Required(ErrorMessage = "Username is missing")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public required string PasswordHash { get; set; }
}