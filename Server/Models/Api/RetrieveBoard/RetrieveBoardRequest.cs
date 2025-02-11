using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.RetrieveBoard;

public class RetrieveBoardRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Board Id is missing")]
    public required long BoardId { get; set; }
}