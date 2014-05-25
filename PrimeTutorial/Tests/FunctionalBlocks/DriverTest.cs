using System;
using NUnit.Framework;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.FunctionalBlocks;

namespace PrimeTutorial.Tests.FunctionalBlocks
{
    [TestFixture]
    class DriverTest
    {
        private const double epsilon = 1.0E-10;

        private static object[] SuccessCases =
        {
            new object[] { new ManipulatorPosition(0, 3, -Math.PI / 2),  new ManipulatorAngles(Math.PI / 2, Math.PI, Math.PI) },
            new object[] { new ManipulatorPosition(1, 0, Math.PI / 2),  new ManipulatorAngles(Math.PI / 2, Math.PI / 2, Math.PI / 2) },
            new object[] { new ManipulatorPosition(2, 1, 0),  new ManipulatorAngles(Math.PI / 2, Math.PI / 2, Math.PI) }
        };

        private static ManipulatorPosition failManipulatorPosition = new ManipulatorPosition(0, 4, -Math.PI/2);

        [Test, TestCaseSource("SuccessCases")]
        public void TestSuccess(ManipulatorPosition input, ManipulatorAngles expected)
        {
            var output = new Driver().Process(input);

            Assert.AreEqual(expected.Betta1, output.Betta1, epsilon);
            Assert.AreEqual(expected.Betta2, output.Betta2, epsilon);
            Assert.AreEqual(expected.Betta3, output.Betta3, epsilon);
        }

        [Test]
        public void TestNanWithoutException()
        {
            var driver = new Driver(false);
            var output = driver.Process(failManipulatorPosition);

            Assert.IsNaN(output.Betta1);
            Assert.IsNaN(output.Betta2);
            Assert.IsNaN(output.Betta3);
        }

        [Test, ExpectedException(typeof(Exception), ExpectedMessage = "Target point is unreachable with given angle: ",
            MatchType = MessageMatch.Contains)]
        public void TestException()
        {
            var driver = new Driver();
            driver.Process(failManipulatorPosition);
        }
    }
}
