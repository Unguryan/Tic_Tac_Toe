using Core.Context.Interfaces;
using Core.Interfaces;
using Core.Models;

namespace Core.Services
{
    public class GameEngine : IGameEngine
    {
        private readonly IGameRepository _gameRepository;

        public GameEngine(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<Game?> GetGame(int id)
        {
            return await _gameRepository.GetGameAsync(id);
        }

        public async Task<Game> MoveGame(int id, int x, int y)
        {
            var game = await _gameRepository.GetGameAsync(id);
            if (game == null) throw new ArgumentNullException(nameof(game));

            if (game.Winner != null)
            {
                return game;
            }

            if (game.BoardToPlay == null)
            {
                game.BoardToPlay = new BoardCoords()
                {
                    X = x,
                    Y = y
                };

                var res = await _gameRepository.SaveGameAsync(game);
                if(res == null)
                {
                    throw new Exception("Game was not saved.(2)");
                }

                return game;
            }

            var coords = game.BoardToPlay;
            var board = game.Boards[coords.X][coords.Y];

            if (board.Winner != null)
            {
                game.BoardToPlay = null;

                var res = await _gameRepository.SaveGameAsync(game);
                if (res == null)
                {
                    throw new Exception("Game was not saved.(3)");
                }

                return game;
            }

            var cell = board.Cells[x][y];
            if (cell.Value != null)
            {
                game.BoardToPlay = null;

                var res = await _gameRepository.SaveGameAsync(game);
                if (res == null)
                {
                    throw new Exception("Game was not saved.(4)");
                }

                return game;
            }

            cell.Value = game.PlayerToMove;
            game.BoardToPlay = new BoardCoords()
            {
                X = x,
                Y = y
            };


            game.PlayerToMove = game.PlayerToMove == PlayerType.White ? PlayerType.Black : PlayerType.White;

            //refactor this.
            board.Winner = CheckIsWinBoard(board);

            if(board.Winner != null)
            {
                game.BoardToPlay = null;
            }

            game.Winner = CheckIsWinGame(game);

            if (game.Boards[x][y].Winner != null)
            {
                game.BoardToPlay = null;
            }

            var resp = await _gameRepository.SaveGameAsync(game);
            if (resp == null)
            {
                throw new Exception("Game was not saved.(5)");
            }

            return game;
        }

        private PlayerType? WinnerToType(Winner? winner)
        {
            if (winner == null)
            {
                return null;
            }

            return (PlayerType?)winner;
        }

        private Winner? CheckIsWinGame(Game game)
        {
            if (game.Winner != null)
            {
                return game.Winner;
            }

            var cells = game.Boards.Select(x => x.Select(y => WinnerToType(y.Winner)).ToArray()).ToArray();

            return IsWinCells(cells);
        }


        private Winner? CheckIsWinBoard(Board board)
        {
            if (board.Winner != null)
            {
                return board.Winner;
            }

            var cells = board.Cells.Select(x => x.Select(y => y.Value).ToArray()).ToArray();

            return IsWinCells(cells);
        }

        private Winner? IsWinCells(PlayerType?[][] cells)
        {
            if (//rows
                (
                cells[0][0] == PlayerType.White &&
                cells[0][1] == PlayerType.White &&
                cells[0][2] == PlayerType.White) ||

                (
                cells[1][0] == PlayerType.White &&
                cells[1][1] == PlayerType.White &&
                cells[1][2] == PlayerType.White) ||

                (
                cells[2][0] == PlayerType.White &&
                cells[2][1] == PlayerType.White &&
                cells[2][2] == PlayerType.White) ||

                //cols
                (
                cells[0][0] == PlayerType.White &&
                cells[1][0] == PlayerType.White &&
                cells[2][0] == PlayerType.White) ||

                (
                cells[0][1] == PlayerType.White &&
                cells[1][1] == PlayerType.White &&
                cells[2][1] == PlayerType.White) ||

                (
                cells[0][2] == PlayerType.White &&
                cells[1][2] == PlayerType.White &&
                cells[2][2] == PlayerType.White) ||

                //cross
                (
                cells[0][0] == PlayerType.White &&
                cells[1][1] == PlayerType.White &&
                cells[2][2] == PlayerType.White) ||

                (
                cells[0][2] == PlayerType.White &&
                cells[1][1] == PlayerType.White &&
                cells[2][0] == PlayerType.White)
                )
            {
                return Winner.White;
            }


            if (//rows
                (
                cells[0][0] == PlayerType.Black &&
                cells[0][1] == PlayerType.Black &&
                cells[0][2] == PlayerType.Black) ||

                (
                cells[1][0] == PlayerType.Black &&
                cells[1][1] == PlayerType.Black &&
                cells[1][2] == PlayerType.Black) ||

                (
                cells[2][0] == PlayerType.Black &&
                cells[2][1] == PlayerType.Black &&
                cells[2][2] == PlayerType.Black) ||

                //cols
                (
                cells[0][0] == PlayerType.Black &&
                cells[1][0] == PlayerType.Black &&
                cells[2][0] == PlayerType.Black) ||

                (
                cells[0][1] == PlayerType.Black &&
                cells[1][1] == PlayerType.Black &&
                cells[2][1] == PlayerType.Black) ||

                (
                cells[0][2] == PlayerType.Black &&
                cells[1][2] == PlayerType.Black &&
                cells[2][2] == PlayerType.Black) ||

                //cross
                (
                cells[0][0] == PlayerType.Black &&
                cells[1][1] == PlayerType.Black &&
                cells[2][2] == PlayerType.Black) ||

                (
                cells[0][2] == PlayerType.Black &&
                cells[1][1] == PlayerType.Black &&
                cells[2][0] == PlayerType.Black)
                )
            {
                return Winner.Black;
            }

            var isDraw = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!cells[i][j].HasValue)
                    {
                        isDraw = false;
                        break;
                    }
                }
            }

            if (isDraw)
            {
                return Winner.Draw;
            }


            return null;
        }

        
    }
}
