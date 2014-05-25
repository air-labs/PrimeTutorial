using Prime;
using System;
using PrimeTutorial.Core.Data;

namespace PrimeTutorial
{
    class Calibrator : IFunctionalBlock<ManipulatorAngles, ManipulatorSignals>
    {
        private static readonly Tuple<double, double> betta1Map = Tuple.Create(0.0, Math.PI);

        private static readonly Tuple<double, double> betta2Map = Tuple.Create(0.0, Math.PI);

        private static readonly Tuple<double, double> betta3Map = Tuple.Create(0.0, Math.PI);

        private static readonly Tuple<int, int> signal = Tuple.Create(0, 255);

        public ManipulatorSignals Process(ManipulatorAngles input)
        {
            return new ManipulatorSignals(
                TranslateAngleToSignal(input.Betta1, betta1Map),
                TranslateAngleToSignal(input.Betta2, betta2Map),
                TranslateAngleToSignal(input.Betta3, betta3Map));
        }

        private int TranslateAngleToSignal(double angle, Tuple<double, double> angleMap)
        {
            var k = (angle - angleMap.Item1) / Math.Abs(angleMap.Item2 - angleMap.Item1);
            return (int) Math.Round(k * Math.Abs(signal.Item2 - signal.Item1) + signal.Item1);
        }
    }
}
