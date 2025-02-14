using Kanboom.Models.CreateUser.DTO;
using Kanboom.Models.Database;

namespace Kanboom.Services.Interfaces;

public interface IUserService {
    Task<CreateUserResponseDTO> CreateUser(CreateUserRequestDTO request);
    Task<long> GetUserIdByUsername(string Username);
    Task<long> GetUserIdByToken(string token);
    Task<List<long?>> GetBoardUsers(long? boardId);
    Task<long> GetBoardOwner(long? boardId);
}  
