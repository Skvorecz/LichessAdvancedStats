using LichessAdvancedStats.Model;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Tests.StatsAgregator
{
	[TestFixture]
	class WhenCalculatingWinrates
	{
		LichessAdvancedStats.Domain.StatsAgregator statsAgregator = new LichessAdvancedStats.Domain.StatsAgregator();

		private Game CreateGame(bool mePlayWhite, string result, params Move[] moves)
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

            game.Moves.AddRange(moves);

            return game;
		}

		[Test]
		public void GameWonOnWhiteCounted()
		{
			var games = new List<Game>
			{
				CreateGame(true, "1-0")
			};

			var stats = statsAgregator.CalculateWinratesByMoves(games, "me");

			stats[0].Victories.Should().Be(1);
		}

		[Test]
		public void GameWonOnBlackCounted()
		{
			var games = new List<Game>
			{
				CreateGame(false, "0-1")
			};

			var stats = statsAgregator.CalculateWinratesByMoves(games, "me");

			stats[0].Victories.Should().Be(1);
		}

		[Test]
		public void GameLostOnWhiteCounted()
		{
			var games = new List<Game>
			{
				CreateGame(true, "0-1")
			};

			var stats = statsAgregator.CalculateWinratesByMoves(games, "me");

			stats[0].Defeats.Should().Be(1);
		}

		[Test]
		public void GameLostOnBlackCounted()
		{
			var games = new List<Game>
			{
				CreateGame(false, "1-0")
			};

			var stats = statsAgregator.CalculateWinratesByMoves(games, "me");

			stats[0].Defeats.Should().Be(1);
		}

		[Test]
		public void DrawCounted([Values]bool result)
		{
			var games = new List<Game>
			{
				CreateGame(result, "1/2-1/2")
			};

			var stats = statsAgregator.CalculateWinratesByMoves(games, "me");

			stats[0].Draws.Should().Be(1);
		}

		[Test]
		public void SeveralGamesWithSameMovesOnSameSideHandled()
		{
			var games = new List<Game>
			{
				CreateGame(true, "1-0", new Move("e4", "e5")),
				CreateGame(true, "0-1", new Move("e4", "e5"))
			};

			var stats = statsAgregator.CalculateWinratesByMoves(games, "me");

			stats[0].Victories.Should().Be(1);
			stats[0].Defeats.Should().Be(1);
		}

		[Test]
		public void SeveralGamesWithDifferentMovesOnSameSideHandled()
        {
			var games = new List<Game>
			{
				CreateGame(true, "1-0", new Move("e4", "e5")),
				CreateGame(true, "0-1", new Move("d4", "d5"))
			};

			var stats = statsAgregator.CalculateWinratesByMoves(games, "me");

			stats.Find(s => s.Moves.SequenceEqual(new Move[] { new Move("e4", "e5") })).Victories.Should().Be(1);
			stats.Find(s => s.Moves.SequenceEqual(new Move[] { new Move("d4", "d5") })).Defeats.Should().Be(1);
		}

		[Test]
		public void ExceptionIsThrownWhenPlayerHaventPlayedGameFromList()
		{
			var game = new Game
			{
				Attributes = new Dictionary<string, string>
				{
					{"White", "NotMe" },
					{"Black", "AlsoNotMe" },
					{"Result", "Draw" }
				}
			};
			var games = new List<Game> { game };

			statsAgregator.Invoking(a => a.CalculateWinratesByMoves(games, "me"))
				.Should()
				.Throw<Exception>();
		}
	}
}