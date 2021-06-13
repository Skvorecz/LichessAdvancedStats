using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LichessAdvancedStats.Model
{
    public class Game
    {
        public Dictionary<string, string> Attributes;
        public List<Move> Moves { get; set; }

        public Game()
        {
            Attributes = new Dictionary<string, string>();
            Moves = new List<Move>();
        }
    }
}
