namespace Kanboom.Models.CreateTask.DTO;

public class CreateTaskRequestDTO 
{

    public string? Token { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public long? Fk_UserAssignee { get; set; }
    public long? Fk_Board { get; set; }
    
}