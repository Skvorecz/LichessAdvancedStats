using LichessAdvancedStats.Model;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;

namespace Tests.StatsAgregator
{
    [TestFixture]
    class WhenCalculatingWinrates
    {
        LichessAdvancedStats.Domain.StatsAgregator StatsAgregator = new LichessAdvancedStats.Domain.StatsAgregator();

        private Game CreateGame(bool mePlayWhite, string result)
        {
            var game = new Game();
            if (mePlayWhite)
            {
                game.Attributes.Add("White", "Me");
                game.Attributes.Add("Black", "NotMe");
            }
            else
            {
                game.Attributes.Add("White", "NotMe");
                game.Attributes.Add("Black", "Me");
            }
            game.Attributes.Add("Result", result);

            return game;
        }

        [Test]
        public void GameWonOnWhiteCounted()
        {
            var games = new List<Game>
            {
                CreateGame(true, "1-0")
            };

            var stats = StatsAgregator.CalculateWinratesByMoves(games, "me");

            stats[0].Victories.Should().Be(1);
        }

        [Test]
        public void GameWonOnBlackCounted()
        {
            var games = new List<Game>
            {
                CreateGame(false, "0-1")
            };

            var stats = StatsAgregator.CalculateWinratesByMoves(games, "me");

            stats[0].Victories.Should().Be(1);
        }

        [Test]
        public void GameLostOnWhiteCounted()
        {
            var games = new List<Game>
            {
                CreateGame(true, "0-1")
            };

            var stats = StatsAgregator.CalculateWinratesByMoves(games, "me");

            stats[0].Defeats.Should().Be(1);
        }

        [Test]
        public void GameLostOnBlackCounted()
        {
            var games = new List<Game>
            {
                CreateGame(false, "1-0")
            };

            var stats = StatsAgregator.CalculateWinratesByMoves(games, "me");

            stats[0].Defeats.Should().Be(1);
        }

        [Test]
        public void DrawCounted([Values]bool result)
        {
            var games = new List<Game>
            {
                CreateGame(result, "1/2-1/2")
            };

            var stats = StatsAgregator.CalculateWinratesByMoves(games, "me");

            stats[0].Draws.Should().Be(1);
        }
    }
}
