using Prime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new LibertyFactory();
            var block = factory.CreateChain(new Driver()).Link(new Calibrator()).ToFunctionalBlock();

            Console.WriteLine(block.Process(Tuple.Create(1.0, 1.0, Math.PI / 4)));
        }
    }
}
