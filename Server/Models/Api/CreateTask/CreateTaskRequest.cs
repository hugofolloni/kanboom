using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.CreateTask;

public class CreateTaskRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Title is missing")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "Description is missing")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "Fk_UserAssigned is missing")]
    public required long Fk_UserAssigned { get; set; }

    [Required(ErrorMessage = "Fk_Board is missing")]
    public required long Fk_Board { get; set; }
}