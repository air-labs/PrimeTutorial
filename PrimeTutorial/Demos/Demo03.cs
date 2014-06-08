using System;
using Prime;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.FunctionalBlocks;

namespace PrimeTutorial.Demos
{
    public static class Demo03
    {
        public static void Run()
        {
            var factory = new LibertyFactory();

            var driver = new Driver();
            var driverChain = factory.CreateChain(driver);

            var emulator = new ManipulatorEmulator();

            var manipulatorChain = driverChain.Link(emulator);

            var manipulatorBlock = manipulatorChain.ToFunctionalBlock();

            ManipulatorPosition[] positions =
            {
                new ManipulatorPosition(0.0, 3.0, -Math.PI / 2),
                new ManipulatorPosition(1.0, 1.0, Math.PI / 4),
                new ManipulatorPosition(3.0, 0.0, 0)
            };

            foreach (var position in positions)
            {
                var status = manipulatorBlock.Process(position);
                Console.WriteLine(status);
            }
        }
    }
}
