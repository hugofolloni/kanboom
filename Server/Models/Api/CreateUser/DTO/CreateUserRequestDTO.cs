namespace Kanboom.Models.CreateUser.DTO;

public class CreateUserRequestDTO
{
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string Email { get; set; }
}