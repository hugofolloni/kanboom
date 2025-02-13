using Kanboom.Models.Database;
using Kanboom.Services.Interfaces;
using Kanboom.Repositories.Interfaces;
using Kanboom.Models;

namespace Kanboom.Services;
 
public class GroupService : IGroupService {
    private readonly IGroupRepository _repository;

    public GroupService(IGroupRepository repository){
        _repository = repository;
    }

    public async Task<List<Domain.Group>> GetGroupsByUser(long userId){
        var response = new List<Domain.Group>();
        
        var groupInfo = await _repository.GetGroupsByUser(userId);
        
        foreach (Group group in groupInfo)
        {
            var userGroup = new Domain.Group{Name = group.Name, GroupLink = group.GroupLink, Fk_OwnerId = group.Fk_OwnerId};
            response.Add(userGroup);
        }

        return response;
    }

    public async Task<List<long?>> GetUsersFromGroup(long? groupId)
    {
        return await _repository.GetUsersFromGroup(groupId);
    }
}   