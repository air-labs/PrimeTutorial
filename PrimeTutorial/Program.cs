using Prime;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.Extensions;
using PrimeTutorial.Core.FunctionalBlocks;
using System;

namespace PrimeTutorial
{
    static class Program
    {
        static void Main(string[] args)
        {
            var factory = new LibertyFactory();

            var driverChain = factory.CreateChain(new Driver(false));

            var emulatorChain = factory.CreateChain(new ManipulatorEmulator());

            Func<ManipulatorAngles, bool> condition = angles =>
                !Double.IsNaN(angles.Betta1) && !Double.IsNaN(angles.Betta2) && !Double.IsNaN(angles.Betta3);
            var ifChain = factory.IfThenElse(condition, emulatorChain, false);

            var block = driverChain.Link(ifChain).ToFunctionalBlock();

            Console.WriteLine(block.Process(new ManipulatorPosition(1.0, 1.0, Math.PI / 4)));
            Console.WriteLine(block.Process(new ManipulatorPosition(0.0, 3.0, -Math.PI / 2)));
            Console.WriteLine(block.Process(new ManipulatorPosition(3.0, 3.0, -Math.PI / 4)));
            Console.WriteLine(block.Process(new ManipulatorPosition(3.0, 0.0, 0)));
        }
    }
}
