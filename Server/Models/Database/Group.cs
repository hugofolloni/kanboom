namespace Kanboom.Models.Database;

public class Group 
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string GroupLink { get; set; }
    public long Fk_OwnerId { get; set; }
    public ICollection<Board>? Board { get; set; }
 }