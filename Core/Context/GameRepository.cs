using Core.Context.DBOs;
using Core.Context.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public class GameRepository : IGameRepository
    {
        private readonly GameContext _context;

        public GameRepository(GameContext context)
        {
            _context = context;
        }

        public async Task<Game?> GetGameAsync(int id)
        {
            return ToModel(await _context.Games.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<int?> SaveGameAsync(Game game)
        {
            var gameDbo = await _context.Games.FirstOrDefaultAsync(x => x.Id == game.Id);

            if(gameDbo == null)
            {
                var resp = await _context.Games.AddAsync(ToDbo(game));
                var temp = await _context.SaveChangesAsync();

                return temp > 0 ? resp.Entity.Id : null;
            }

            //refactor it
            gameDbo.Winner = game.Winner;
            gameDbo.PlayerToMove = game.PlayerToMove;
            if (game.BoardToPlay == null)
            {
                gameDbo.X_BoardToPlay = null;
                gameDbo.Y_BoardToPlay = null;
            }
            else
            {
                gameDbo.X_BoardToPlay = game.BoardToPlay.X;
                gameDbo.Y_BoardToPlay = game.BoardToPlay.Y;
            }

            gameDbo.Boards = new BoardDbo[3][];
            for (int i = 0; i < 3; i++)
            {
                gameDbo.Boards[i] = new BoardDbo[3];
                for (int j = 0; j < 3; j++)
                {
                    gameDbo.Boards[i][j] = new BoardDbo()
                    {
                        Cells = game.Boards[i][j].Cells.Select(x => x.Select(y => y.Value).ToArray()).ToArray(),
                        Winner = game.Boards[i][j].Winner
                    };
                }
            }

            var res = await _context.SaveChangesAsync();
            return gameDbo.Id;
        }


        //Move converting to another class
        
        private GameDbo? ToDbo(Game? game)
        {
            if (game == null)
            {
                return null;
            }

            var gameDbo = new GameDbo();

            gameDbo.Id = game.Id;
            gameDbo.Winner = game.Winner;
            gameDbo.PlayerToMove = game.PlayerToMove;
            if (game.BoardToPlay == null)
            {
                gameDbo.X_BoardToPlay = null;
                gameDbo.Y_BoardToPlay = null;
            }
            else
            {
                gameDbo.X_BoardToPlay = game.BoardToPlay.X;
                gameDbo.Y_BoardToPlay = game.BoardToPlay.Y;
            }

            gameDbo.Boards = new BoardDbo[3][];
            for (int i = 0; i < 3; i++)
            {
                gameDbo.Boards[i] = new BoardDbo[3];
                for (int j = 0; j < 3; j++)
                {
                    gameDbo.Boards[i][j] = new BoardDbo()
                    {
                        Cells = game.Boards[i][j].Cells.Select(x => x.Select(y => y.Value).ToArray()).ToArray(),
                        Winner = game.Boards[i][j].Winner
                    };
                }
            }

            return gameDbo;
        }

        private Game? ToModel(GameDbo? dbo)
        {
            if(dbo == null)
            {
                return null;
            }

            var game = new Game();

            game.Id = dbo.Id;
            game.Winner = dbo.Winner;
            game.PlayerToMove = dbo.PlayerToMove;
            if (dbo.X_BoardToPlay == null && dbo.Y_BoardToPlay == null)
            {
                game.BoardToPlay = null;
            }
            else
            {
                game.BoardToPlay = new BoardCoords()
                {
                    X = dbo.X_BoardToPlay.Value,
                    Y = dbo.Y_BoardToPlay.Value,
                };
            }

            game.Boards = new Board[3][];
            for (int i = 0; i < 3; i++)
            {
                game.Boards[i] = new Board[3];
                for (int j = 0; j < 3; j++)
                {
                    game.Boards[i][j] = new Board()
                    {
                        Cells = dbo.Boards[i][j].Cells.Select(x => x.Select(y => new Cell() { Value = y}).ToArray()).ToArray(),
                        Winner = dbo.Boards[i][j].Winner,
                        BoardCoords = new BoardCoords()
                        {
                            X = i,
                            Y = j
                        }
                    };

                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            game.Boards[i][j].Cells[x][y].X = x;
                            game.Boards[i][j].Cells[x][y].Y = y;
                        }
                    }
                }
            }

            return game;
        }
    }
}
