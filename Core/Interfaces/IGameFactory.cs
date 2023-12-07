using Core.Models;

namespace Core.Interfaces
{
    public interface IGameFactory
    {
        Task<Game> CreateGame();
    }
}