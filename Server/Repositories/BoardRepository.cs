using Kanboom.Repositories.Interfaces;
using Kanboom.Context;
using Microsoft.EntityFrameworkCore;
using Kanboom.Models.Database;
using Kanboom.Models.CreateBoard.DTO;
using System.Security.Cryptography;

namespace Kanboom.Repositories;
public class BoardRepository : IBoardRepository {
    private readonly AppDbContext _context;

    public BoardRepository(AppDbContext context){
        _context = context;
    }

    public async Task<IEnumerable<Board>> GetBoardsByUser(long userId)
    {     
         return await _context.Board
        .Join(_context.BoardUser,
            Board => Board.Id,
            boardUser => boardUser.Fk_BoardId,
            (Board, boardUser) => new { Board, boardUser })
        .Where(joined => joined.boardUser.Fk_UserId == userId)
        .Select(joined => new Board
        {
            Id = joined.Board.Id,
            Name = joined.Board.Name,
            StagesCount = joined.Board.StagesCount,
            IsGroupBoard = joined.Board.IsGroupBoard,
            Fk_BoardOwner = joined.Board.Fk_BoardOwner,
            Invite = joined.Board.Invite
        })
        .ToListAsync();
    }

    public async Task<Board> CreateBoard(CreateBoardRequestDTO request, long userId)
    {
         var board = new Board
        {
            Name = request.Name,
            StagesCount = 3,
            Fk_BoardOwner = userId,
            IsGroupBoard = false,
            Invite = GenerateRandomCode(8)
        };
        _context.Board.Add(board);

        var result = await _context.SaveChangesAsync();

        var boardUser = new BoardUser{
            Fk_BoardId = board.Id,
            Fk_UserId = userId
        };
        _context.BoardUser.Add(boardUser);

        _context.StageLevels.Add(new StageLevels{ StageName = "To-Do", StageNumber = 0, Fk_Board = board.Id});
        _context.StageLevels.Add(new StageLevels{ StageName = "Doing", StageNumber = 1, Fk_Board = board.Id});
        _context.StageLevels.Add(new StageLevels{ StageName = "Done", StageNumber = 2, Fk_Board = board.Id});

        await _context.SaveChangesAsync();
        
        if (result == 0)
        {
            return null;
        }

        return board;
    }

    public async Task<bool> CheckUser(long userId, long? boardId)
    {
        return await _context.BoardUser
            .AnyAsync(ub => ub.Fk_UserId == userId && ub.Fk_BoardId == boardId);
    }

    public async Task<Board> RetrieveBoard(long? boardId)
    {
        return await _context.Board
        .FirstOrDefaultAsync(b => b.Id == boardId);
    }

    public async Task<List<long?>> GetBoardUsers(long? boardId) 
    {
        return await _context.BoardUser
        .Where(gu => gu.Fk_BoardId == boardId)
        .Select(gu => gu.Fk_UserId)
        .ToListAsync();
    }

    public async Task<List<StageLevels>> RetrieveLabels(long? boardId)
    {
        return await _context.StageLevels
        .Where(sl => sl.Fk_Board == boardId)
        .ToListAsync();
    }

    public async Task<bool> AddUserToBoard(long? userId, long? boardId)
    {
        try
        {
            var userBoard = new BoardUser
            {
                Fk_UserId = userId,
                Fk_BoardId = boardId
            };

            _context.BoardUser.Add(userBoard);
            await _context.SaveChangesAsync();
            
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<long?> RetrieveBoardIdByInvite(string? invite)
    {
        return await _context.Board
        .Where(b => b.Invite == invite)
        .Select(b => (long?)b.Id)
        .FirstOrDefaultAsync();
    }
    
    private static string GenerateRandomCode(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] result = new char[length];

        using var rng = RandomNumberGenerator.Create();
        byte[] data = new byte[length];

        rng.GetBytes(data);

        for (int i = 0; i < length; i++)
        {
            result[i] = chars[data[i] % chars.Length];
        }

        return new string(result);
    }

    public async Task<bool> RemoveUserFromBoard(long? userId, long? boardId)
    {
        try
        {
            var userBoard = await _context.BoardUser
            .Where(ub => ub.Fk_UserId == userId && ub.Fk_BoardId == boardId)
            .FirstOrDefaultAsync();

            _context.BoardUser.Remove(userBoard);
            await _context.SaveChangesAsync();
            
            return true;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<Models.Database.Task>> GetTasksByUserInBoard(long userId, long boardId)
    {
        return await _context.Task
        .Where(t => t.Fk_UserAssigned == userId && t.Fk_Board == boardId)
        .ToListAsync();
    }
}