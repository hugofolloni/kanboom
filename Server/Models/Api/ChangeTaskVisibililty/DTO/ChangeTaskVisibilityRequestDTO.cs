namespace Kanboom.Models.ChangeTaskVisibilityRequestDTO.DTO;

public class ChangeTaskVisibilityRequestDTO 
{
    public long? Id { get; set; }
    public required bool Hidden { get; set; }
    public string? Token { get; set; }
    public long? Fk_Board { get; set; }

}