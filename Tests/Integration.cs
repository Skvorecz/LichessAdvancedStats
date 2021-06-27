using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LichessAdvancedStats.Domain;
using NUnit.Framework;
using Parser = LichessAdvancedStats.Domain.PgnParser;
using Agregator = LichessAdvancedStats.Domain.StatsAgregator;

namespace Tests
{
    [TestFixture]
    class Integration
    {
        [Test]
        public async Task Full()
        {
            var worker = new LichessApiWorker();
            var parser = new Parser();
            var agregator = new Agregator();

            var pgn = await worker.LoadUsersGamesPgnAsync("skvorec");
            var games = parser.Parse(pgn);
            var stat = agregator.CalculateWinratesByMoves(games, "skvorec", 1);

            
        }
    }
}
