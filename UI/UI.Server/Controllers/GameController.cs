using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace UI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameFactory _gameFactory;
        private readonly IGameEngine _gameEngine;

        public GameController(IGameFactory gameFactory, IGameEngine gameEngine) 
        {
            _gameFactory = gameFactory;
            _gameEngine = gameEngine;
        }

        [HttpGet]
        [Route("create")]
        public async Task<Game> CreateGame()
        {
            return await _gameFactory.CreateGame();
        }

        [HttpGet]
        [Route("move")]
        public async Task<Game> MoveGame(int id, int x, int y)
        {
            return await _gameEngine.MoveGame(id, x, y);
        }

        [HttpGet]
        [Route("get")]
        public async Task<Game?> GetGame(int id)
        {
            return await _gameEngine.GetGame(id);
        }
    }
}
