namespace Kanboom.Models.ChangeTaskAssignee.DTO;

public class ChangeTaskAssigneeRequestDTO 
{
    public long Id { get; set; }
    public required long Assignee { get; set; }
    public string? Token { get; set; }
    public long? Fk_Board { get; set; }

}