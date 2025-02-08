using Kanboom.Models.AuthLogin.DTO;
using Kanboom.Models.PersistUser.DTO;
using Kanboom.Models.Database;

namespace Kanboom.Services.Interfaces;

public interface IAuthService {
    Task<AuthLoginResponseDTO> CheckLogin(AuthLoginRequestDTO request);
    PersistUserResponseDTO ValidateToken(PersistUserRequestDTO token);
    string GenerateJwtToken(User user);
}
