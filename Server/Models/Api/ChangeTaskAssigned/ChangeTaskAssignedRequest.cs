using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.ChangeTaskAssigned;

public class ChangeTaskAssignedRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Id is missing")]
    public required long Id { get; set; }

    [Required(ErrorMessage = "Fk_Board is missing")]
    public required long Fk_Board { get; set; }

    [Required(ErrorMessage = "Assigned is missing")]
    public required long Assigned { get; set; }
}