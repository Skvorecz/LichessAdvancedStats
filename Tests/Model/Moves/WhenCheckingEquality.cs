using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using LichessAdvancedStats.Model;


namespace Tests.Model.Moves
{
    [TestFixture]
    public class WhenCheckingEquality
    {
        [Test]
        public void MovesAreEqualWhenBlackAndWhiteMovesAreSame()
        {
            var leftMove = new Move { WhiteMove = "e4", BlackMove = "e5" };
            var rightMove = new Move { WhiteMove = "e4", BlackMove = "e5" };

            var equal = leftMove.Equals(rightMove);

            equal.Should().BeTrue();
        }

        [TestCase("e4","e5","d4","d5")]
        [TestCase("e4","e5","e4","d5")]
        [TestCase("e4","e5","d4","e5")]
        public void MovesAreNotEqualWhenBlackAndWhiteMovesAreDifferent(string leftWhiteMove, string leftBlackMove, string rightWhiteMove, string rightBlackMove)
        {
            var leftMove = new Move { WhiteMove = leftWhiteMove, BlackMove = leftBlackMove };
            var rightMove = new Move { WhiteMove = rightWhiteMove, BlackMove = rightBlackMove };

            var equal = leftMove.Equals(rightMove);

            equal.Should().BeFalse();
        }

        [Test]
        public void EqualAreFalseWhenObjectHasWrongType()
        {
            var move = new Move();
            var obj = new object();

            var equals = move.Equals(obj);

            equals.Should().BeFalse();
        }
    }
}
