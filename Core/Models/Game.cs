namespace Core.Models
{
    public class Game
    {
        public int Id { get; set; }

        public Board[][] Boards { get; set; }

        public BoardCoords? BoardToPlay { get; set; }

        public PlayerType PlayerToMove { get; set; }

        public Winner? Winner { get; set; }

    }
}
