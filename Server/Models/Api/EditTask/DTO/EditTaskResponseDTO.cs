namespace Kanboom.Models.EditTask.DTO;

public class EditTaskResponseDTO 
{
    public Domain.Task? Task { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}