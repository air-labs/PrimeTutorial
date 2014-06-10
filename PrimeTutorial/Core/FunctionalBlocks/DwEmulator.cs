using System;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Prime;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.Forms;
using PrimeTutorial.Core.FormsData;

namespace PrimeTutorial.Core.FunctionalBlocks
{
    class DwEmulator : IFunctionalBlock<Dwm, bool>
    {
        private readonly DwForm _form = new DwForm();

        private const int Delay = 100;
        private const double Base = 1;
        private const double Delta = 0.1;

        private const Double Epsilon = 0.0000001;

        private readonly Matrix _matrix = new Matrix();
        private double _angle;

        public DwEmulator()
        {
            new Thread(() => Application.Run(_form)).Start();
        }

        public bool Process(Dwm input)
        {
            if (Math.Abs(input.Time) < Epsilon)
            {
                return true;
            }

            var time = 0d;
            var delta = Delta;

            while (Math.Abs(delta) > 0.001)
            {
                delta = time + Delta < input.Time ? Delta : input.Time - time;
                CalculatePosition(input, time, delta);

                time += delta;
                Thread.Sleep(Delay);
            }

            return true;
        }

        private void CalculatePosition(Dwm dwm, double time, double delta)
        {
            var vLeft = time * (dwm.VLeftFinish - dwm.VLeftStart) / dwm.Time + dwm.VLeftStart;
            var vRight = time * (dwm.VRgihtFinish - dwm.VRightStart) / dwm.Time + dwm.VRightStart;

            double radius, v, offset;
            if (Math.Abs(vLeft + vRight) < 0.0001)
            {
                radius = Base;
                v = Math.Sign(vLeft - vRight) * (Math.Abs(vLeft) + Math.Abs(vRight));
                offset = 0;
            }
            else
            {
                radius = Base * (Math.Abs(vLeft) + Math.Abs(vRight)) / (Math.Abs(vLeft) - Math.Abs(vRight));
                v = (vLeft + vRight) / 2.0;
                offset = v;
            }

            var w = v / radius;

            var angle = -w * delta;
            var y = Math.Cos(angle) * offset * delta;
            var x = Math.Sin(angle) * offset * delta;

            _matrix.Rotate((float) (angle * 180 / Math.PI));
            _matrix.Translate((float) x, (float) y);

            _angle += angle;

            _form.SetPosition(new FormDwPosition(_matrix.OffsetX, _matrix.OffsetY, _angle));
        }
    }
}
