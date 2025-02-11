using Kanboom.Models;

namespace Kanboom.Services.Interfaces;

public interface IGroupService {
    Task<List<Domain.Group>> GetGroupsByUser(long userId);
    Task<List<long?>> GetUsersFromGroup(long? groupId);
}
