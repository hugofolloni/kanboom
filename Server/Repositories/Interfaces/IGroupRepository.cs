using Kanboom.Models.Database;

namespace Kanboom.Repositories.Interfaces;

public interface IGroupRepository
{
    Task<IEnumerable<Group>> GetGroupsByUser(long userId);
    Task<List<long?>> GetUsersFromGroup(long? groupId);
}