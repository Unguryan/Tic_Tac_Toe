namespace Core.Models
{
    public class Board
    {
        public BoardCoords BoardCoords { get; set; }

        public Winner? Winner { get; set; }

        public Cell[][] Cells { get; set; }
    }
}
