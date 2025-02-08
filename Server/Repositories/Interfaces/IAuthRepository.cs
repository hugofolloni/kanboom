using Kanboom.Models.Database;
using Kanboom.Models.AuthUser.DTO;

namespace Kanboom.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<User> GetUser(AuthUserRequestDTO request);
}