namespace Kanboom.Models.Database;

public class User 
{
    public long Id { get; set; }
    public string? Username { get; set; }
    public string? Password{ get; set; }
    public string? Email { get; set; }
    public ICollection<Task>? Task { get; set; }

    public ICollection<UserGroup>? UserGroup { get; set; }

    public ICollection<BoardUser>? BoardUser  { get; set; }
}