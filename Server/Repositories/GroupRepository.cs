using Kanboom.Repositories.Interfaces;
using Kanboom.Context;
using Microsoft.EntityFrameworkCore;
using Kanboom.Models.Database;

namespace Kanboom.Repositories;
public class GroupRepository : IGroupRepository {
    private readonly AppDbContext _context;

    public GroupRepository(AppDbContext context){
        _context = context;
    }

    public async Task<IEnumerable<Group>> GetGroupsByUser(long userId){     
         return await _context.Group
        .Join(_context.UserGroup,
            group => group.Id,
            userGroup => userGroup.Fk_GroupId,
            (group, userGroup) => new { group, userGroup })
        .Where(joined => joined.userGroup.Fk_UserId == userId)
        .Select(joined => new Group
        {
            Id = joined.group.Id,
            Name = joined.group.Name,
            GroupLink = joined.group.GroupLink
        })
        .ToListAsync();
    }

    public async Task<List<long?>> GetUsersFromGroup(long? groupId)
    {
        return await _context.UserGroup
            .Where(gu => gu.Fk_GroupId == groupId)
            .Select(gu => gu.Fk_UserId)
            .ToListAsync();
    }
}