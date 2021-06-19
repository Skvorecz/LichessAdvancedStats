using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LichessAdvancedStats.Model
{
    public class Stats
    {
        public Stats(List<Move> moves)
        {
            Moves = moves;
        }

        public List<Move> Moves { get; set; }
        public int Victories { get; set; }
        public int Draws { get; set; }
        public int Defeats { get; set; }
    }
}
