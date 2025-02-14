namespace Kanboom.Models.ChangeTaskStage.DTO;

public class ChangeTaskStageRequestDTO 
{
    public long? Id { get; set; }
    public required int Stage { get; set; }
    public string? Token { get; set; }
    public long? Fk_Board { get; set; }

}