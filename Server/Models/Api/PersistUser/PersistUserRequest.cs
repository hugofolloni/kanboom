using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.PersistUser;

public class PersistUserRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }
}