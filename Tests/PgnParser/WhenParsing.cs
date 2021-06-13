using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using LichessAdvancedStats.Domain;
using LichessAdvancedStats.Model;

namespace Tests.PgnParser
{
    [TestFixture]
    class WhenParsing
    {
        LichessAdvancedStats.Domain.PgnParser parser = new LichessAdvancedStats.Domain.PgnParser();

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
    }
}
