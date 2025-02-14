using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.ChangeBoardOwner;

public class ChangeBoardOwnerRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "BoardId is missing")]
    public required long BoardId { get; set; }

    [Required(ErrorMessage = "Owner is missing")]
    public required long Owner { get; set; }
}