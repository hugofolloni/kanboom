using Kanboom.Models.DTO;

namespace Kanboom.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<UserResponseDTO> GetUser(UserRequestDTO request);
}