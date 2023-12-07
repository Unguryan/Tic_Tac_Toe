using Core.Models;

namespace Core.Context.Interfaces
{
    public interface IGameRepository
    {
        Task<Game?> GetGameAsync(int id);

        Task<int?> SaveGameAsync(Game game);
    }
}
