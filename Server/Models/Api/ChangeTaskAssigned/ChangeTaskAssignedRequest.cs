using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.ChangeTaskAssignee;

public class ChangeTaskAssigneeRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Id is missing")]
    public required long Id { get; set; }

    [Required(ErrorMessage = "Fk_Board is missing")]
    public required long Fk_Board { get; set; }

    [Required(ErrorMessage = "Assignee is missing")]
    public required long Assignee { get; set; }
}