using System.ComponentModel;
namespace Kanboom.Models.AuthLogin.DTO;

public class AuthLoginResponseDTO
{
    [DefaultValue("")]
    public string? Token { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}