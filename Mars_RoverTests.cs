using NUnit.Framework;
using Mars_Rover;

namespace Mars_Rover.UnitTests
{
    [TestFixture]
    public class ProgramTests
    {
        private Program _program;

        [SetUp]
        public void SetUp()
        {
            _program = new Program();
        }

        [Test]
        public void TestParseRepeat()
        {
            bool testYes = _program.ParseRepeat("Y");
            bool testNo = _program.ParseRepeat("gu");
            bool testEmpty = _program.ParseRepeat("");

            Assert.IsTrue(testYes);
            Assert.IsFalse(testNo);
            Assert.IsFalse(testEmpty);
        }
    }
}