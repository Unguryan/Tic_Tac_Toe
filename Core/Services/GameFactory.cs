using Core.Context.Interfaces;
using Core.Interfaces;
using Core.Models;

namespace Core.Services
{
    public class GameFactory : IGameFactory
    {
        private readonly IGameRepository _gameRepository;

        public GameFactory(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<Game> CreateGame()
        {
            var game = new Game();

            game.BoardToPlay = null;
            game.Winner = null;

            game.PlayerToMove = PlayerType.White;
            game.Boards = new Board[3][];

            for (int i = 0; i < 3; i++)
            {
                game.Boards[i] = new Board[3];
                for (int j = 0; j < 3; j++)
                {
                    game.Boards[i][j] = new Board()
                    {
                        Cells = InitCells(),
                        Winner = null
                    };
                }
            }

            var res = await _gameRepository.SaveGameAsync(game);

            //refactor this
            if (res == null)
            {
                throw new Exception("Game was not created");
            }

            return await _gameRepository.GetGameAsync(res.Value);
        }

        private Cell[][] InitCells()
        {
            var cells = new Cell[3][];

            for (int i = 0; i < 3; i++)
            {
                cells[i] = new Cell[3];
                for (int j = 0; j < 3; j++)
                {
                    cells[i][j] = new Cell()
                    {
                        X = i,
                        Y = j,
                        Value = null
                    };
                }
            }

            return cells;
        }
    }
}
