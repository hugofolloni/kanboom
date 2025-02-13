using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.LeaveBoard.DTO;

public class LeaveBoardRequestDTO : BaseRequest
{
    public string? Token { get; set; }
    public long? BoardId { get; set; }
}