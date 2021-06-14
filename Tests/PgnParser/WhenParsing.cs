using FluentAssertions;
using NUnit.Framework;
using LichessAdvancedStats.Model;
using System.Collections.Generic;

namespace Tests.PgnParser
{
    [TestFixture]
    public class WhenParsing
    {
        private LichessAdvancedStats.Domain.PgnParser parser = new LichessAdvancedStats.Domain.PgnParser();

        [Test]
        public void OneAttributeParsed()
        {
            var pgn = "[Event \"Rated Blitz game\"]" +
                "\n1. e4 e5 2. f4 exf4";

            var games = parser.Parse(pgn);

            games[0].Attributes["Event"].Should().Be("Rated Blitz game");
            games[0].Attributes.Count.Should().Be(1);
        }

        [Test]
        public void SeveralAttributesParsed()
        {
            var pgn = "[White \"Skvorec\"]" +
                "\n[Black \"MarsellusW\"]" +
                "\n1. e4 e5 2. f4 exf4";

            var games = parser.Parse(pgn);

            games[0].Attributes["White"].Should().Be("Skvorec");
            games[0].Attributes["Black"].Should().Be("MarsellusW");
            games[0].Attributes.Count.Should().Be(2);
        }

        [Test]
        public void MovesParsed()
        {
            var pgn = "[White \"Skvorec\"]" +
                "\n[Black \"MarsellusW\"]" +
                "\n1. e4 e5 2. f4 exf4";

            var games = parser.Parse(pgn);

            games[0].Moves[0].WhiteMove.Should().Be("e4");
            games[0].Moves[0].BlackMove.Should().Be("e5");
            games[0].Moves[1].WhiteMove.Should().Be("f4");
            games[0].Moves[1].BlackMove.Should().Be("exf4");
        }

        [Test]
        public void PgnWithSeveralGamesParsed()
        {
            var pgn = "[White \"ceretron\"]" +
                "\n[Black \"Skvorec\"]" +
                "\n\n1. e4 e5 2. Nf3 Nc6" +
                "\n\n\n[White \"shakenbake677\"]" +
                "[Black \"Skvorec\"]" +
                "1. e4 e5 2. Qh5 Nc6";
            var expectedFirstGame = new Game
            {
                Attributes = new Dictionary<string, string>
                {
                    {"White", "ceretron" },
                    {"Black", "Skvorec" }
                },
                Moves = new List<Move>
                {
                    new Move("e4", "e5"),
                    new Move("Nf3", "Nc6")
                }
            };
            var expectedSecondGame = new Game
            {
                Attributes = new Dictionary<string, string>
                {
                    {"White", "shakenbake677" },
                    {"Black", "Skvorec" }
                },
                Moves = new List<Move>
                {
                    new Move("e4", "e5"),
                    new Move("Qh5", "Nc6")
                }
            };

            var games = parser.Parse(pgn);

            games[0].Should().Be(expectedFirstGame);
            games[1].Should().Be(expectedSecondGame);
        }
    }
}
