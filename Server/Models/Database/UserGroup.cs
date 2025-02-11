namespace Kanboom.Models.Database;

public class UserGroup 
{
    public long Id { get; set; }
    public long? Fk_UserId { get; set; }
    public long? Fk_GroupId { get; set; }
}