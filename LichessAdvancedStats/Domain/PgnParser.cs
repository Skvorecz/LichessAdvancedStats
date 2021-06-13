using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LichessAdvancedStats.Model;

namespace LichessAdvancedStats.Domain
{
    public class PgnParser
    {
        public List<Game> Parse(string pgn)
        {
            var games = new List<Game>();

            var game = new Game();

            var attributes = pgn.Split('[', ']');

            foreach (var attribute in attributes)
            {
                var preparedString = attribute.Trim();

                var splited = preparedString.Split('"');

                if (splited.Length > 1)
                {
                    game.Attributes.Add(splited[0].Trim(),
                                        splited[1].Trim());
                }
            }

            games.Add(game);

            return games;    
        }
    }
}
