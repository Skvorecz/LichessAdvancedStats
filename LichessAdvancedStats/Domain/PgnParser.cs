using System.Collections.Generic;
using System.Linq;
using LichessAdvancedStats.Model;
using System.Text.RegularExpressions;

namespace LichessAdvancedStats.Domain
{
    public class PgnParser
    {
        private const string gamePattern = @"((\[.*\])\s*)+([^/[])+";
        private const string attributePattern = @"\[[^\[]*\]";
        private const string keyPattern = @"\w+[^\u0022\s]";
        private const string valuePattern = @"\u0022[\w\s]+\u0022";

        public List<Game> Parse(string pgn)
        {
            var result = new List<Game>();

            var gameRegex = new Regex(gamePattern);
            var games = gameRegex.Matches(pgn);
            foreach (var game in games)
            {
                result.Add(ParseGame(game.ToString()));
            }

            return result;
        }

        private Game ParseGame(string gameString)
        {
            var attributes = ParseAttributes(gameString);
            var moves = ParseMoves(gameString);

            return new Game
            {
                Attributes = attributes,
                Moves = moves
            };
        }

        private Dictionary<string, string> ParseAttributes(string gameString)
        {
            var result = new Dictionary<string, string>();

            var attributeRegex = new Regex(attributePattern);
            var attributes = attributeRegex.Matches(gameString.ToString());

            foreach (var attribute in attributes)
            {
                ParseSingleAttribute(attribute.ToString(),
                    out var key,
                    out var value);

                result.Add(key, value);
            }

            return result;
        }

        private void ParseSingleAttribute(string attribute, out string key, out string value)
        {
            var keyRegex = new Regex(keyPattern);
            key = keyRegex.Match(attribute.ToString()).Value;

            var valueRegex = new Regex(valuePattern);
            value = valueRegex.Match(attribute).Value.Replace("\"", "");
        }

        private List<Move> ParseMoves(string gameString)
        {
            var movesStartIndex = gameString.LastIndexOf(']') + 1;
            var movesString = gameString.Substring(movesStartIndex);

            var movesSplited = movesString.Trim().Split(" ");
            var moves = new List<Move>();   
            
            for (int i = 0; i < movesSplited.Length; i++)
            {
                if (i % 3 == 0)
                {
                    moves.Add(new Move());
                    continue;
                }
                if (i % 3 == 1)
                {
                    moves.Last().WhiteMove = movesSplited[i];
                    continue;
                }
                if (i % 3 == 2)
                {
                    moves.Last().BlackMove = movesSplited[i];
                    continue;
                }
            }

            return moves;
        }
    }
}