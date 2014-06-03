using System;

namespace PrimeTutorial.Core.Data
{
    [Serializable]
    public class Dwm
    {
        public double VLeft0 { get; private set; }
        public double VRight0 { get; private set; }

        public double VLeft1 { get; private set; }
        public double VRgiht1 { get; private set; }

        public double Time { get; private set; }

        public Dwm(double vLeft0, double vRight0, double vLeft1, double vRgiht1, double time)
        {
            VLeft0 = vLeft0;
            VRight0 = vRight0;
            VLeft1 = vLeft1;
            VRgiht1 = vRgiht1;
            Time = time;
        }
    }
}
