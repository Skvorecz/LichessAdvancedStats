using System;
using System.Collections.Generic;
using System.Linq;
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
                var stats = GetOrCreateNewStats(numberOfMovesToCompare, statsList, game);
                CountGameResult(playerName, game, stats);
            }

            return statsList;
        }

        private Stats GetOrCreateNewStats(int numberOfMovesToCompare, List<Stats> statsList, Game game)
        {
            var firstMoves = game.Moves.Take(numberOfMovesToCompare).ToList();
            var stats = statsList.Find(s => s.Moves.SequenceEqual(firstMoves));
            if (stats == null)
            {
                stats = new Stats(firstMoves);
                statsList.Add(stats);
            }

            return stats;
        }

        private void CountGameResult(string playerName, Game game, Stats stats)
        {
            if (game.Result == GameResult.Draw)
            {
                stats.Draws++;
            }

            if (GameWasWon(playerName, game))
            {
                stats.Victories++;
            }
            else
            {
                stats.Defeats++;
            }
        }

        private bool GameWasWon(string playerName, Game game)
        {
            return (IsPlayerPlayAsWhite(game, playerName) && game.Result == GameResult.WhiteVictory)
                            || (!IsPlayerPlayAsWhite(game, playerName) && game.Result == GameResult.BlackVictory);
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