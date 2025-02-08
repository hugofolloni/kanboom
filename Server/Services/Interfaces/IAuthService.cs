using Kanboom.Models.AuthUser.DTO;

namespace Kanboom.Services.Interfaces {
    public interface IAuthService {
        Task<AuthUserResponseDTO> CheckLogin(AuthUserRequestDTO request);
    }
}