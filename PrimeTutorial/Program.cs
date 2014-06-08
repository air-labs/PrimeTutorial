using Prime;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.FunctionalBlocks;
using System;

namespace PrimeTutorial
{
    static class Program
    {
        static void Main(string[] args)
        {
            var factory = new LibertyFactory();
            var block = factory.CreateChain(new Driver()).Link(new ManipulatorEmulator()).ToFunctionalBlock();

            Console.WriteLine(block.Process(new ManipulatorPosition(1.0, 1.0, Math.PI / 4)));
        }
    }
}
