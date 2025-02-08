using Kanboom.Repositories.Interfaces;
using Kanboom.Models.AuthLogin.DTO;
using Kanboom.Context;
using Microsoft.EntityFrameworkCore;
using Kanboom.Models.Database;
namespace Kanboom.Repositories;
public class AuthRepository : IAuthRepository {
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context){
        _context = context;
    }

    public async Task<User> GetUser(AuthLoginRequestDTO request){     
        return await (_context.User ?? Enumerable.Empty<User>().AsQueryable())
        .Where(x => x.Username == request.Username)
        .FirstOrDefaultAsync();
    }
}