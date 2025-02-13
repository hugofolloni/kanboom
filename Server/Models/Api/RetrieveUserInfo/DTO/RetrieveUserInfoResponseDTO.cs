namespace Kanboom.Models.RetrieveUserInfo.DTO;

public class RetrieveUserInfoResponseDTO
{
    public string? Username { get; set; }
    public long? Id { get; set; }
    public List<Domain.Board>? Boards { get; set; }
    public List<Domain.Task>? Tasks { get; set; }
    public string? ProfilePic { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}