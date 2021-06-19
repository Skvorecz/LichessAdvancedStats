using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LichessAdvancedStats.Model;

namespace Tests.Model.Games
{
    [TestFixture]
    class WhenGettingResult
    {
        [Test]
        public void WhiteVictory()
        {
            var game = new Game
            {
                Attributes = new Dictionary<string, string>
                {
                    {"Result", "1-0" }
                }
            };

            var result = game.Result;

            result.Should().Be(GameResult.WhiteVictory);
        }

        [Test]
        public void BlakckVictory()
        {
            var game = new Game
            {
                Attributes = new Dictionary<string, string>
                {
                    {"Result", "0-1" }
                }
            };

            var result = game.Result;

            result.Should().Be(GameResult.BlackVictory);
        }

        [Test]
        public void Draw()
        {
            var game = new Game
            {
                Attributes = new Dictionary<string, string>
                {
                    {"Result", "1/2-1/2" }
                }
            };

            var result = game.Result;

            result.Should().Be(GameResult.Draw);
        }
    }
}
