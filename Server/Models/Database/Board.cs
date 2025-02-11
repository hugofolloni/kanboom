namespace Kanboom.Models.Database;

public class Board 
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required int StagesCount { get; set; }
    public long? Fk_GroupId { get; set; }
    public required long Fk_BoardOwner { get; set; }
    public required bool IsGroupBoard { get; set; }
    public required string Invite { get; set; }

    public ICollection<Task >? Task { get; set; }
    public ICollection<BoardUser>? BoardUser { get; set; }
    public ICollection<StageLevels>? StageLevels { get; set; }
}