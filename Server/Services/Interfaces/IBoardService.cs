using Kanboom.Models;
using Kanboom.Models.ChangeBoardOwner.DTO;
using Kanboom.Models.ChangeBoardStages.DTO;
using Kanboom.Models.CreateBoard.DTO;
using Kanboom.Models.LeaveBoard.DTO;
using Kanboom.Models.RetrieveBoard.DTO;

namespace Kanboom.Services.Interfaces;

public interface IBoardService {
    Task<List<Domain.Board>> GetBoardsByUser(long userId);
    Task<CreateBoardResponseDTO> CreateBoard(CreateBoardRequestDTO request);
    Task<RetrieveBoardResponseDTO> RetrieveBoard(RetrieveBoardRequestDTO request);
    Task<RetrieveBoardResponseDTO> AddUserToBoard(HandleInviteRequestDTO request);
    Task<LeaveBoardResponseDTO> LeaveBoard(LeaveBoardRequestDTO request);
    Task<ChangeBoardOwnerResponseDTO> ChangeOwner(ChangeBoardOwnerRequestDTO request);
    Task<ChangeBoardStagesResponseDTO> AddStageToBoard(ChangeBoardStagesRequestDTO request);
    Task<ChangeBoardStagesResponseDTO> RemoveStageFromBoard(ChangeBoardStagesRequestDTO request);
    Task<ChangeBoardStagesResponseDTO> RenameStage(ChangeBoardStagesRequestDTO request);
}
