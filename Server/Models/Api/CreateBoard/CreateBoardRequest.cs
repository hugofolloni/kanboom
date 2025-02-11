using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.CreateBoard;

public class CreateBoardRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Name is missing")]
    public required string Name { get; set; }
}