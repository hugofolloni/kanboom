using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models.GetUserGroups;

public class GetUserGroupsResponse : BaseRequest
{
    public List<Domain.Group>? Groups { get; set; }
    
}