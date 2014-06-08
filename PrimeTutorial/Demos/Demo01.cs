using System;
using Prime;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.FunctionalBlocks;

namespace PrimeTutorial.Demos
{
    public static class Demo01
    {
        public static void Run()
        {
            var factory = new LibertyFactory();

            var driver = new Driver();
            var driverChain = factory.CreateChain(driver);

            var calibrator = new Calibrator();
            var calibratorChain = factory.CreateChain(calibrator);

            var manipulatirChain = factory.LinkChainToChain(driverChain, calibratorChain);
            
            var manipulatorBlock = manipulatirChain.ToFunctionalBlock();

            var position = new ManipulatorPosition(1.0, 1.0, Math.PI / 4);
            var signals = manipulatorBlock.Process(position);

            Console.WriteLine(signals);
        }
    }
}
