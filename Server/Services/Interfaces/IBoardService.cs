using Kanboom.Models;
using Kanboom.Models.CreateBoard.DTO;
using Kanboom.Models.RetrieveBoard.DTO;

namespace Kanboom.Services.Interfaces;

public interface IBoardService {
    Task<List<Domain.Board>> GetBoardsByUser(long userId);
    Task<CreateBoardResponseDTO> CreateBoard(CreateBoardRequestDTO request);
    Task<RetrieveBoardReponseDTO> RetrieveBoard(RetrieveBoardRequestDTO request);
    Task<RetrieveBoardReponseDTO> AddUserToBoard(HandleInviteRequestDTO request);
}
