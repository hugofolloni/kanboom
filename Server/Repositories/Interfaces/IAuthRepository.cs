using Kanboom.Models.Database;
using Kanboom.Models.AuthLogin.DTO;

namespace Kanboom.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<User> GetUser(AuthLoginRequestDTO request);
}