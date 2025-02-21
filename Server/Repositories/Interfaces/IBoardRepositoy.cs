using Kanboom.Models.ChangeBoardStages.DTO;
using Kanboom.Models.CreateBoard.DTO;
using Kanboom.Models.Database;

namespace Kanboom.Repositories.Interfaces;

public interface IBoardRepository
{
    Task<IEnumerable<Board>> GetBoardsByUser(long userId);
    Task<Board> CreateBoard(CreateBoardRequestDTO request, long userId);
    Task<bool> CheckUser(long userId, long? boardId);
    Task<Board> RetrieveBoard(long? boardId);
    Task<List<long?>> GetBoardUsers(long? boardId); 
    Task<List<StageLevels>> RetrieveLabels(long? boardId);
    Task<bool> AddUserToBoard(long? userId, long? boardId);
    Task<long?> RetrieveBoardIdByInvite(string? invite);
    Task<bool> RemoveUserFromBoard(long? userId, long? boardId);
    Task<List<Models.Database.Task>> GetTasksByUserInBoard(long userId, long boardId);
    Task<Board> ChangeOwner(long newOwnerId, long boardId);
    Task<bool> DeleteBoard(long? boardId);
    Task<long> GetBoardOwner(long? boardId);
    Task<bool> UpdateStageNumber(long boardId, int currentStage, int newStage);
    Task<bool> AddStageToBoard(ChangeBoardStagesRequestDTO request);
}