using Prime;
using System;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.FunctionalBlocks;

namespace PrimeTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new LibertyFactory();
            var block = factory.CreateChain(new Driver()).Link(new Calibrator()).ToFunctionalBlock();

            Console.WriteLine(block.Process(new ManipulatorPosition(1.0, 1.0, Math.PI / 4)));
        }
    }
}
