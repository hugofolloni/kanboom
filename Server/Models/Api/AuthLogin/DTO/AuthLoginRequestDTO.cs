namespace Kanboom.Models.AuthLogin.DTO;

public class AuthLoginRequestDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}