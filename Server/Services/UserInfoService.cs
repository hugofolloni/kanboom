using Kanboom.Models.PersistUser.DTO;
using Kanboom.Models.RetrieveUserInfo.DTO;
using Kanboom.Services.Interfaces;

namespace Kanboom.Services;
 
public class UserInfoService : IUserInfoService {

    private readonly IUserService _userService;
    private readonly IGroupService _groupService;
    private readonly IBoardService _boardService;
    private readonly IAuthService _authService;

    public UserInfoService(IUserService userService, IGroupService groupService, IBoardService boardService, IAuthService authService){
        _userService = userService;
        _groupService = groupService;
        _boardService = boardService;
        _authService = authService;
    }

    public async Task<RetrieveUserInfoResponseDTO> RetrieveInfo(RetrieveUserInfoRequestDTO request)
    {
        var response = new RetrieveUserInfoResponseDTO();
        
        try {
            
            var validation = _authService.ValidateToken(new PersistUserRequestDTO{Token = request.Token});
            if(!validation.IsSuccessful) throw new Exception("INVALID_JWT_TOKEN");

            response.Username = validation.Username;

            var userId = await _userService.GetUserIdByUsername(validation.Username);

            response.Groups = await _groupService.GetGroupsByUser(userId);
            response.Boards = await _boardService.GetBoardsByUser(userId);
            response.Id = userId;

            response.IsSuccessful = true;
            return response;
        }
        catch(Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            response.Username = null;
            response.Groups = null;
            response.Boards = null;
            return response;
        }
    }

}