using NUnit.Framework;
using Mars_Rover;

namespace Mars_Rover.UnitTests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void TestParseRepeat()
        {
            bool testYes = Program.ParseRepeat("Y");
            bool testNo = Program.ParseRepeat("gu");
            bool testEmpty = Program.ParseRepeat("");

            Assert.IsTrue(testYes);
            Assert.IsFalse(testNo);
            Assert.IsFalse(testEmpty);
        }

        [Test]
        public void TestCheckInstructions()
        {
            bool testOk = Program.CheckInstructions("LLMR");
            bool testEmpty = Program.CheckInstructions("");
            bool testBad = Program.CheckInstructions("xxx");

            Assert.IsTrue(testOk);
            Assert.IsFalse(testBad);
            Assert.IsTrue(testEmpty);
        }

        [Test]
        public void TestParseGridInput()
        {
            var gridOk = Program.ParseGridInput("3 5");
            var gridBad = Program.ParseGridInput("hello");
            
            
        }
    }
}