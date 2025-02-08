using System.ComponentModel;
namespace Kanboom.Models.AuthUser.DTO;

public class AuthUserResponseDTO 
{
    [DefaultValue("")]
    public string? Token { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}