namespace Kanboom.Models.ChangeBoardOwner.DTO;

public class ChangeBoardOwnerRequestDTO 
{
    public long BoardId { get; set; }
    public required long BoardOwner { get; set; }
    public string? Token { get; set; }

}