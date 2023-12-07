using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.Context.DBOs
{
    public class GameDbo
    {
        [Key]
        public int Id { get; set; }

        public BoardDbo[][] Boards { get; set; }

        public int? X_BoardToPlay { get; set; }

        public int? Y_BoardToPlay { get; set; }

        public PlayerType PlayerToMove { get; set; }

        public Winner? Winner { get; set; }
    }
}
