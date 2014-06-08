using System;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework.Constraints;
using Prime;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.Forms;

namespace PrimeTutorial.Core.FunctionalBlocks
{
    class DwEmulator : IFunctionalBlock<Dwm, bool>
    {
        private readonly DwForm _form = new DwForm();

        private const int Delay = 100;

        public DwEmulator()
        {
            new Thread(() => Application.Run(_form)).Start();
        }

        public bool Process(Dwm input)
        {
            double vl = input.VLeft1;
            double vr = input.VRgiht1;

            double b = 1;
            double time = 0.1;

            double r, v, offset;
            if (Math.Abs(vl + vr) < 0.0001)
            {
                r = b;
                v = Math.Abs(vl) + Math.Abs(vr);
                offset = 0;
            }
            else
            {
                r = b * (Math.Abs(vl) + Math.Abs(vr)) / (Math.Abs(vl) - Math.Abs(vr));
                v = (vl + vr) / 2.0;
                offset = v;
            }

            double w = v / r;
            var steps = (int) input.Time / time;
            for (var i = 0; i < steps; i++)
            {
                double angle = -w * time;
                double y = Math.Cos(angle) * offset * time;
                double x = Math.Sin(angle) * offset * time;

                _form.PutDwm(Tuple.Create(x, y, angle * 180 / Math.PI));
                Thread.Sleep(Delay);
            }

            return true;
        }
    }
}
