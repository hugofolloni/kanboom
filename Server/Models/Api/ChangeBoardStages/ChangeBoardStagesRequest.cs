using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.ChangeBoardStages;

public class ChangeBoardStagesRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "BoardId is missing")]
    public required long BoardId { get; set; }

    [Required(ErrorMessage = "StageNumber is missing")]
    public required int StageNumber{ get; set; }

    [Required(ErrorMessage = "StageName is missing")]
    public required string StageName { get; set; }
}