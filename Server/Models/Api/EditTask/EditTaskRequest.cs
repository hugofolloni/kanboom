using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.EditTask;

public class EditTaskRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Id is missing")]
    public required long Id { get; set; }

    [Required(ErrorMessage = "Fk_Board is missing")]
    public required long Fk_Board { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public long? Fk_UserAssignee { get; set; }

}