using Kanboom.Models.AuthLogin.DTO;
using Kanboom.Models.PersistUser.DTO;
namespace Kanboom.Services.Interfaces;

public interface IAuthService {
    Task<AuthLoginResponseDTO> CheckLogin(AuthLoginRequestDTO request);
    PersistUserResponseDTO ValidateToken(PersistUserRequestDTO token);
}
