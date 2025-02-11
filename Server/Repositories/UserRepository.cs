using Kanboom.Repositories.Interfaces;
using Kanboom.Models.CreateUser.DTO;
using Kanboom.Context;
using Microsoft.EntityFrameworkCore;
using Kanboom.Models.Database;
namespace Kanboom.Repositories;

public class UserRepository : IUserRepository {
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context){
        _context = context;
    }
    public async Task<User> CreateUser(CreateUserRequestDTO request)
    {
         var user = new User
        {
            Username = request.Username,
            Password = request.PasswordHash,
            Email = request.Email
        };

        _context.User.Add(user);
        var result = await _context.SaveChangesAsync();
        
        if (result == 0)
        {
            return null;
        }

        return user;
    }

    public async Task<User> GetUserByUsername(string username) {
        return await (_context.User ?? Enumerable.Empty<User>().AsQueryable())
        .Where(x => x.Username == username)
        .FirstOrDefaultAsync();
    }
}