namespace Kanboom.Models.CreateTask.DTO;

public class CreateTaskResponseDTO 
{
    public Domain.Task? Task { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}