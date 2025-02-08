namespace Kanboom.Models.Database;

public class User 
{
    public long Id { get; set; }
    public required string Username { get; set; }
    public required string Password{ get; set; }
    public required string Email { get; set; }
}