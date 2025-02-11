using Kanboom.Models.Database;
using Kanboom.Models.CreateUser.DTO;

namespace Kanboom.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> CreateUser(CreateUserRequestDTO request);
    Task<User> GetUserByUsername(string username);
}