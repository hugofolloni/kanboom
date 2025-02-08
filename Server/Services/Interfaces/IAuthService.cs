using Kanboom.Models.DTO;

namespace Kanboom.Services.Interfaces {
    public interface IAuthService {
        Task<UserResponseDTO> CheckLogin(UserRequestDTO request);
    }
}