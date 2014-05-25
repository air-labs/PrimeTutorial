using System;

namespace PrimeTutorial.Core.Data
{
    [Serializable]
    public class ManipulatorAngles
    {
        public double Betta1 { get; private set; }

        public double Betta2 { get; private set; }

        public double Betta3 { get; private set; }

        public ManipulatorAngles(double betta1, double betta2, double betta3)
        {
            Betta1 = betta1;
            Betta2 = betta2;
            Betta3 = betta3;
        }
    }
}
