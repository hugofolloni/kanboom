using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.RetrieveBoard;

public class HandleInviteRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Invite is missing")]
    public required string Invite { get; set; }
}