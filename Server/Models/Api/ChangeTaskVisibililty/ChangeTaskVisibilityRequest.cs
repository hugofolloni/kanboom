using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.ChangeTaskVisibility;

public class ChangeTaskVisibilityRequest : BaseRequest
{
    [Required(ErrorMessage = "Token is missing")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Id is missing")]
    public required long Id { get; set; }

    [Required(ErrorMessage = "Fk_Board is missing")]
    public required long Fk_Board { get; set; }

    [Required(ErrorMessage = "Hidden is missing")]
    public required bool Hidden { get; set; }
}