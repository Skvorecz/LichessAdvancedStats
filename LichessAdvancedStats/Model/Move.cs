using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LichessAdvancedStats.Model
{
    public class Move
    {
        public string WhiteMove { get; set; }
        public string BlackMove { get; set; }

        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var move = (Move)obj;
            return move.WhiteMove == this.WhiteMove
                && move.BlackMove == this.BlackMove;
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{WhiteMove} {BlackMove}";
        }
    }
}
