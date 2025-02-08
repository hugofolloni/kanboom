namespace Kanboom.Models.Database;

public class StageLevels 
{
    public long Id { get; set; }
    public required string StageName  { get; set; }
    public required long Fk_Board { get; set; }
    public required int StageNumber { get; set; }
}