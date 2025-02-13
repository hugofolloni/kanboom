using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.LeaveBoard;

public class LeaveBoardRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Board Id is missing")]
    public required long BoardId { get; set; }
}