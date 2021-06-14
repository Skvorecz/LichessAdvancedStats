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





            var gameStartIndex = 0;
            var movesIndex = pgn.IndexOf("1.", gameStartIndex);
            var gameEndIndex = pgn.IndexOf('[', movesIndex) - 1;

            gameEndIndex = gameEndIndex > 0
                ? gameEndIndex
                : pgn.Length - 1;


            var game = ParseGame(pgn, gameStartIndex, movesIndex, gameEndIndex);
            games.Add(game);

            return games;
        }

        private Game ParseGame(string pgn, int gameStartIndex, int movesIndex, int gameEndIndex)
        {
            var game = new Game();
            game.Attributes = ParseAttributes(pgn, gameStartIndex, movesIndex);
            game.Moves = ParseMoves(pgn, movesIndex, gameEndIndex);
            return game;
        }

        private Dictionary<string, string> ParseAttributes(string pgn, int gameStartIndex, int movesIndex)
        {
            var attributes = pgn.Substring(gameStartIndex, movesIndex - gameStartIndex).Split('[', ']');
            var attributesDictionary = new Dictionary<string, string>();
            foreach (var attribute in attributes)
            {
                var preparedString = attribute.Trim();

                var splited = preparedString.Split('"');

                if (splited.Length > 1)
                {
                    attributesDictionary.Add(splited[0].Trim(),
                                        splited[1].Trim());
                }
            }

            return attributesDictionary;
        }

        private List<Move> ParseMoves(string pgn, int movesIndex, int gameEndIndex)
        {
            var moves = new List<Move>();

            var movesString = pgn.Substring(movesIndex, gameEndIndex - movesIndex + 1);
            var movesSplited = movesString.Split(" ");
            for (int i = 0; i < movesSplited.Length; i++)
            {
                if (i % 3 == 0)
                {
                    moves.Add(new Move());
                }
                if (i % 3 == 1)
                {
                    moves.Last().WhiteMove = movesSplited[i];
                }
                if (i % 3 == 2)
                {
                    moves.Last().BlackMove = movesSplited[i];
                }
            }

            return moves;
        }

        
    }
}
