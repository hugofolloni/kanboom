using Kanboom.Models.CreateUser.DTO;
using Kanboom.Models.Database;
using Kanboom.Models.PersistUser.DTO;
using Kanboom.Repositories.Interfaces;
using Kanboom.Services.Interfaces;

namespace Kanboom.Services;
 
public class UserService : IUserService {
    private readonly IUserRepository _repository;
    private readonly IAuthService _authService;
    private readonly IBoardRepository _boardRepository;
    private readonly IGroupService _groupService;

    public UserService(IUserRepository repository, IAuthService authService, IBoardRepository boardRepository, IGroupService groupService){
        _repository = repository;
        _authService = authService;
        _boardRepository = boardRepository;
        _groupService = groupService;
    }

    public async Task<CreateUserResponseDTO> CreateUser(CreateUserRequestDTO request)
    {
        var response = new CreateUserResponseDTO();
        try{
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
        catch(Exception ex){
            response.IsSuccessful = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public async Task<long> GetUserIdByUsername(string username){
        var data = await _repository.GetUserByUsername(username);

        return data.Id;
    }

    public async Task<long> GetUserIdByToken(string token){
        var username = _authService.ValidateToken(new PersistUserRequestDTO{Token = token}).Username;
        Console.Write("username = ", username);
        return await GetUserIdByUsername(username);
    }

    public async Task<List<long?>> GetBoardUsers(long? boardId)
    {
        var data = await _boardRepository.RetrieveBoard(boardId);

        if(data.IsGroupBoard){
            return await _groupService.GetUsersFromGroup(data.Fk_GroupId);
        }

        return await _boardRepository.GetBoardUsers(boardId);
    }

    public async Task<long> GetBoardOwner(long? boardId){
        return await _boardRepository.GetBoardOwner(boardId);
    }

}