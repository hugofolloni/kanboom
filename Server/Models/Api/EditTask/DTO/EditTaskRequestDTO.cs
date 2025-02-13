namespace Kanboom.Models.EditTask.DTO;

public class EditTaskRequestDTO 
{
    public long? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public long? Fk_UserAssigned { get; set; }
    public string? Token { get; set; }
    public long? Fk_Board { get; set; }

}