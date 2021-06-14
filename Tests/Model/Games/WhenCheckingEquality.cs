using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using LichessAdvancedStats.Model;

namespace Tests.Model.Games
{
    [TestFixture]
    public class WhenCheckingEquality
    {
        [Test]
        public void GamesAreEqualWhenAttributesAndMovesAreSame()
        {
            var leftGame = new Game
            {
                Attributes = new Dictionary<string, string> { { "Event", "Rated Blitz game" },
                                                                {"Date", "2021.06.14" }},
                Moves = new List<Move> { new Move() { WhiteMove = "e4", BlackMove = "e5"},
                                        new Move() {WhiteMove = "Nc3", BlackMove = "g6"}}
            };
            var rightGame = new Game
            {
                Attributes = new Dictionary<string, string> { { "Event", "Rated Blitz game" },
                                                                {"Date", "2021.06.14" }},
                Moves = new List<Move> { new Move() { WhiteMove = "e4", BlackMove = "e5"},
                                        new Move() {WhiteMove = "Nc3", BlackMove = "g6"}}
            };

            var equals = leftGame.Equals(rightGame);

            equals.Should().BeTrue();
        }

        [Test]
        public void GamesAreNotEqualWhenTheyHasDifferentAttributesCount()
        {
            var leftGame = new Game
            {
                Attributes = new Dictionary<string, string> { { "Event", "Rated Blitz game" },
                                                                {"Date", "2021.06.14" } }
            };
            var rightGame = new Game
            {
                Attributes = new Dictionary<string, string> { { "Event", "Rated Blitz game" } }
            };

            var equals = leftGame.Equals(rightGame);

            equals.Should().BeFalse();
        }

        [Test]
        public void GamesAreNotEqualWhenTheyHasDifferentAttributes()
        {
            var leftGame = new Game
            {
                Attributes = new Dictionary<string, string> { { "Event", "Rated Blitz game" } }
            };
            var rightGame = new Game
            {
                Attributes = new Dictionary<string, string> { { "Event", "Rated Rapid game" } }
            };

            var equals = leftGame.Equals(rightGame);

            equals.Should().BeFalse();
        }

        [Test]
        public void GamesAreNotEqualWhenTheyHasDifferentMovesCount()
        {
            var leftGame = new Game
            {
                Moves = new List<Move> { new Move { WhiteMove = "e4", BlackMove = "e5"},
                                        new Move {WhiteMove = "Nc3", BlackMove = "g6"} }
            };
            var rightGame = new Game
            {
                Moves = new List<Move> { new Move { WhiteMove = "e4", BlackMove = "e5"} }
            };

            var equals = leftGame.Equals(rightGame);

            equals.Should().BeFalse();
        }

        [Test]
        public void GamesAreNotEqualWhenTheyHasDifferentMoves()
        {
            var leftGame = new Game
            {
                Moves = new List<Move> { new Move { WhiteMove = "e4", BlackMove = "e5"} }
            };
            var rightGame = new Game
            {
                Moves = new List<Move> { new Move { WhiteMove = "d4", BlackMove = "d5" } }
            };

            var equals = leftGame.Equals(rightGame);

            equals.Should().BeFalse();
        }

        [Test]
        public void EqualAreFalseWhenObjectHasWrongType()
        {
            var game = new Game();
            var obj = new object();

            var equals = game.Equals(obj);

            equals.Should().BeFalse();
        }
    }
}
