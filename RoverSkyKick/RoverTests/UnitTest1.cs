using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover2;

namespace RoverTests
{
    [TestClass]
    public class UnitTest1
    {
        private RoverSkykick _roverSkykick;

        [TestMethod]
        public void TestMethod1()
        {
            var args = new[]
            {
                "5 5",
                "1 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM"
            };
            _roverSkykick = new RoverSkykick(args);
        }
    }
}
