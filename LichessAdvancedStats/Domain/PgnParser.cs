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

            var pgnEndIndex = pgn.Length - 1;
            int gameStartIndex;
            int movesIndex;
            int gameEndIndex = -1;


            do
            {
                gameStartIndex = gameEndIndex + 1;
                movesIndex = pgn.IndexOf("1.", gameStartIndex);

                gameEndIndex = pgn.IndexOf('[', movesIndex) - 1;
                gameEndIndex = gameEndIndex > 0
                    ? gameEndIndex
                    : pgnEndIndex;

                var gameString = pgn.Substring(gameStartIndex, gameEndIndex - gameStartIndex + 1);
                var game = ParseGame(gameString);
                games.Add(game);
            }
            while (gameEndIndex < pgnEndIndex);
                        

            return games;
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
