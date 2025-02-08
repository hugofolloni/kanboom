namespace Kanboom.Models.DTO;

public class UserResponseDTO 
{
    public string? Token { get; set; }
    public bool? IsSuccessful { get; set; }
    public string? Message { get; set; }
}