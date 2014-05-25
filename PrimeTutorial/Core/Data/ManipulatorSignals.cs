using System;

namespace PrimeTutorial.Core.Data
{
    [Serializable]
    public class ManipulatorSignals
    {
        public int Shoulder { get; private set; }

        public int Elbow { get; private set; }

        public int Palm { get; private set; }

        public ManipulatorSignals(int shoulder, int elbow, int palm)
        {
            Shoulder = shoulder;
            Elbow = elbow;
            Palm = palm;
        }
    }
}
