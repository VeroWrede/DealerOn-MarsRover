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


    }
}