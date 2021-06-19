using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LichessAdvancedStats.Model;

namespace LichessAdvancedStats.Domain
{
    public class StatsAgregator
    {
        public List<Stats> CalculateWinratesByMoves(List<Game> games, string playerName, int numberOfMovesToCompare)
        {
            var statsList = new List<Stats>();

            foreach (var game in games)
            {
                var firstMoves = game.Moves.Take(numberOfMovesToCompare).ToList();
                var stats = statsList.Find(s => s.Moves.SequenceEqual(firstMoves));
                if (stats == null)
                {
                    stats = new Stats(firstMoves);
                    statsList.Add(stats);
                }

                if (game.Result == GameResult.Draw)
                {
                    stats.Draws++;
                }

                if (IsPlayerPlayAsWhite(game, playerName) && game.Result == GameResult.WhiteVictory
                    || !IsPlayerPlayAsWhite(game, playerName) && game.Result == GameResult.BlackVictory)
                {
                    stats.Victories++;
                }
                else
                {
                    stats.Defeats++;
                }
            }

            return statsList;
        }

        private bool IsPlayerPlayAsWhite(Game game, string playerName)
        {
            var whitePlayerName = game.White;
            var blackPlayerName = game.Black;
            if (string.Compare(whitePlayerName, playerName, true) == 0)
            {
                return true;
            }
            else if ((string.Compare(blackPlayerName, playerName, true) == 0))
            {
                return false;
            }
            else
            {
                throw new Exception($"Player {playerName} haven't played this game");
            }
        }
    }
}
