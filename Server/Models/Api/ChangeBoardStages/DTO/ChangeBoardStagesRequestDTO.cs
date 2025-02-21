namespace Kanboom.Models.ChangeBoardStages.DTO;

public class ChangeBoardStagesRequestDTO 
{
    public long BoardId { get; set; }
    public required int StageNumber { get; set; }
    public required string StageName { get; set; }
    public string? Token { get; set; }
}