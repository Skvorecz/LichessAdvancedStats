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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var game = (Game)obj;
            
            if(game.Moves.Count != this.Moves.Count)
            {
                return false;
            }

            for(var i = 0; i < game.Moves.Count; i++)
            {
                if(game.Moves[i].Equals(this.Moves[i]) == false)
                {
                    return false;
                }
            }


            if(game.Attributes.Count != this.Attributes.Count)
            {
                return false;
            }

            foreach(var pair in game.Attributes)
            {
                if(!this.Attributes.ContainsKey(pair.Key)
                    || this.Attributes[pair.Key] != pair.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {            
            return base.GetHashCode();
        }
    }
}
