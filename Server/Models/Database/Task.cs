namespace Kanboom.Models.Database;

public class Task 
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public long? Fk_Board { get; set; }
    public required int StageNumber { get; set; }
    public  long?  Fk_UserAssigned  { get; set; }
    public bool Hidden { get; set; }
}