using Prime;
using System;

namespace PrimeTutorial
{
    class Calibrator : IFunctionalBlock<Tuple<double, double, double>, Tuple<int, int, int>>
    {
        private static readonly Tuple<double, double> betta1Map = Tuple.Create(0.0, Math.PI);

        private static readonly Tuple<double, double> betta2Map = Tuple.Create(0.0, Math.PI);

        private static readonly Tuple<double, double> betta3Map = Tuple.Create(0.0, Math.PI);

        private static readonly Tuple<int, int> signal = Tuple.Create(0, 255);

        public Tuple<int, int, int> Process(Tuple<double, double, double> input)
        {
            return Tuple.Create(
                TranslateAngleToSignal(input.Item1, betta1Map),
                TranslateAngleToSignal(input.Item2, betta2Map),
                TranslateAngleToSignal(input.Item3, betta3Map));
        }

        private int TranslateAngleToSignal(double angle, Tuple<double, double> angleMap)
        {
            var k = (angle - angleMap.Item1) / Math.Abs(angleMap.Item2 - angleMap.Item1);
            return (int) Math.Round(k * Math.Abs(signal.Item2 - signal.Item1) + signal.Item1);
        }
    }
}
