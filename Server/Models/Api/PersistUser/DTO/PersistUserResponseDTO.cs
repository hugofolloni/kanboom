using System.ComponentModel;
namespace Kanboom.Models.PersistUser.DTO;

public class PersistUserResponseDTO 
{
    [DefaultValue("")]
    public string? Username { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}