using System;
using PrimeTutorial.Demos;

namespace PrimeTutorial
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            #region Demonstration #1: linking chains by LibertyPrime

            Console.WriteLine("Demonstration #1: linking chains by LibertyPrime");
            Demo01.Run();
            Console.WriteLine();

            #endregion

            #region Demonstration #2: manual linking blocks

            Console.WriteLine("Demonstration #2: manual linking blocks");
            Demo02.Run();
            Console.WriteLine();

            #endregion

            #region Demonstration #3: emulator

            Console.WriteLine("Demonstration #3: emulator");
            Demo03.Run();
            Console.WriteLine();

            #endregion

            #region Demonstration #4: if-then-else

            Console.WriteLine("Demonstration #4: if-then-else");
            Demo04.Run();
            Console.WriteLine();

            #endregion
        }
    }
}
