using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LichessAdvancedStats.Model;
using System.Text.RegularExpressions;

namespace LichessAdvancedStats.Domain
{
    public class PgnParser
    {
        private const string patternForGame = @"((\[.*\])\s*)+([^/[])+";
        private const string patternForAttribute = @"\[[^\[]*\]";
        private const string keyPattern = @"\w+[^\u0022\s]";
        private const string valuePattern = @"\u0022[\w\s]+\u0022";

        public List<Game> Parse(string pgn)
        {
            var gamesResult = new List<Game>();

            var gameRegex = new Regex(patternForGame, RegexOptions.IgnorePatternWhitespace);
            var games = gameRegex.Matches(pgn);
            foreach (var game in games)
            {
                var gameDomain = new Game();
                var attributeRegex = new Regex(patternForAttribute);
                var attributes = attributeRegex.Matches(game.ToString());
                foreach (var attribute in attributes)
                {
                    var keyRegex = new Regex(keyPattern);
                    var key = keyRegex.Match(attribute.ToString()).Value;

                    var valueRegex = new Regex(valuePattern);
                    var value = valueRegex.Match(attribute.ToString()).Value.Replace("\"", "");

                    gameDomain.Attributes.Add(key, value);
                }

                var movesStartIndex = game.ToString().LastIndexOf(']') + 1;
                var movesString = game.ToString().Substring(movesStartIndex);
                var moves = ParseMoves(movesString);
                gameDomain.Moves = moves;

                gamesResult.Add(gameDomain);
            }

            return gamesResult;
        }

        private Game ParseGame(string gameString)
        {
            var movesIndex = gameString.IndexOf("1.");
            var game = new Game
            {
                Attributes = ParseAttributes(gameString.Substring(0, movesIndex - 1)),
                Moves = ParseMoves(gameString.Substring(movesIndex))
            };
            return game;
        }

        private Dictionary<string, string> ParseAttributes(string attributes)
        {
            var attributesSplited = attributes.Split('[', ']');
            var attributesDictionary = new Dictionary<string, string>();
            foreach (var attribute in attributesSplited)
            {
                var splited = attribute.Trim().Split('"');

                if (splited.Length > 1)
                {
                    attributesDictionary.Add(splited[0].Trim(),
                                        splited[1].Trim());
                }
            }

            return attributesDictionary;
        }

        private List<Move> ParseMoves(string movesString)
        {
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
