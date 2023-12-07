using Core.Models;
namespace Core.Context.DBOs
{
    public class BoardDbo
    {
        public Winner? Winner { get; set; }

        public PlayerType?[][] Cells { get; set; }
    }
}
