namespace Kanboom.Models.GetUserGroups.DTO;

public class GetUserGroupsResponseDTO
{
    public List<Domain.Group>? Groups { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}