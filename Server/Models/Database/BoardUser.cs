namespace Kanboom.Models.Database;

public class BoardUser 
{
    public long Id { get; set; }
    public long? Fk_UserId { get; set; }
    public long? Fk_BoardId { get; set; }
}