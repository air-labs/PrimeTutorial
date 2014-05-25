using System;

namespace PrimeTutorial.Core.Data
{
    [Serializable]
    public class ManipulatorPosition
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public double Alpha { get; private set; }

        public ManipulatorPosition(double x, double y, double alpha)
        {
            X = x;
            Y = y;
            Alpha = alpha;
        }
    }
}