using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LichessAdvancedStats.Model;

namespace LichessAdvancedStats.Domain
{
    public class StatsAgregator
    {
        public List<Stats> CalculateWinratesByMoves(List<Game> games, string playerName)
        {
            var game = games[0];
            var stats = new Stats();
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

            return new List<Stats> { stats };
        }

        private bool IsPlayerPlayAsWhite(Game game, string playerName)
        {
            var whitePlayerName = game.Attributes["White"];
            return string.Compare(whitePlayerName, playerName, true) == 0;
        }
    }
}
