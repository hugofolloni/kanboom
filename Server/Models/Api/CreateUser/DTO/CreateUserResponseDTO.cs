namespace Kanboom.Models.CreateUser.DTO;

public class CreateUserResponseDTO
{ 
    public string? Username { get; set; }
    public string? Token { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}