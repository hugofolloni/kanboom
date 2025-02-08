using Kanboom.Repositories.Interfaces;
using Kanboom.Services.Interfaces;
using Kanboom.Models.DTO;

namespace Kanboom.Services {
    public class AuthService : IAuthService {
        private readonly IAuthRepository _repository;

        public AuthService(IAuthRepository repository){
            _repository = repository;
        }

        public async Task<UserResponseDTO> CheckLogin(UserRequestDTO request){
            var response = new UserResponseDTO();

            var data = await _repository.GetUser(request);

            return response;
        }

    }
}