using Kanboom.Models.CreateUser.DTO;

namespace Kanboom.Services.Interfaces;

public interface IUserService {
    Task<CreateUserResponseDTO> CreateUser(CreateUserRequestDTO request);
}
