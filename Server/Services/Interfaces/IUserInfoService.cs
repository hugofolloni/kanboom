using Kanboom.Models.RetrieveUserInfo.DTO;

namespace Kanboom.Services.Interfaces;

public interface IUserInfoService {
    Task<RetrieveUserInfoResponseDTO> RetrieveInfo(RetrieveUserInfoRequestDTO request);
}
