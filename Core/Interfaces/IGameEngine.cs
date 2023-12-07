using Core.Models;

namespace Core.Interfaces
{
    public interface IGameEngine
    {
        Task<Game?> GetGame(int id);
        Task<Game> MoveGame(int id, int x, int y);
    }
}