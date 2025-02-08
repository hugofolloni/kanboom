using Kanboom.Models.Database;
using Kanboom.Models.CreateUser.DTO;
using Kanboom.Repositories.Interfaces;
using Kanboom.Services.Interfaces;

namespace Kanboom.Services;
 
public class UserService : IUserService {
    private readonly IUserRepository _repository;
    private readonly IAuthService _authService;

    public UserService(IUserRepository repository, IAuthService authService){
        _repository = repository;
        _authService = authService;
    }

    public async Task<CreateUserResponseDTO> CreateUser(CreateUserRequestDTO request)
    {
        var response = new CreateUserResponseDTO();

        var data = await _repository.CreateUser(request);

        if (data == null){
            response.IsSuccessful = false;
            response.Message = "COULDNT_CREATE_USER";
            return response;
        }

        response.IsSuccessful = true;
        response.Username = data.Username;
        
        var token = _authService.GenerateJwtToken(data);
        response.Token = token;
        
        return response;
    }

}