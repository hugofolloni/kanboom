using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.ChangeTaskStage;

public class ChangeTaskStageRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Id is missing")]
    public required long Id { get; set; }

    [Required(ErrorMessage = "Fk_Board is missing")]
    public required long Fk_Board { get; set; }

    [Required(ErrorMessage = "Stage is missing")]
    public required int Stage { get; set; }
}