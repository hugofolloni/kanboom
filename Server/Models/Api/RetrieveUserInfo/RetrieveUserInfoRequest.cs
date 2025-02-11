using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.RetrieveUserInfo;

public class RetrieveUserInfoRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }
}