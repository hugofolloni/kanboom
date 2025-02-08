namespace Kanboom.Models.AuthUser.DTO;

public class AuthUserRequestDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}