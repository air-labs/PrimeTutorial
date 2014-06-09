using System;

namespace PrimeTutorial.Core.Data
{
    [Serializable]
    public class Dwm
    {
        public double VLeftStart { get; private set; }
        public double VRightStart { get; private set; }

        public double VLeftFinish { get; private set; }
        public double VRgihtFinish { get; private set; }

        public double Time { get; private set; }

        public Dwm(double vLeftStart, double vRightStart, double vLeftFinish, double vRgihtFinish, double time)
        {
            VLeftStart = vLeftStart;
            VRightStart = vRightStart;
            VLeftFinish = vLeftFinish;
            VRgihtFinish = vRgihtFinish;
            Time = time;
        }
    }
}
