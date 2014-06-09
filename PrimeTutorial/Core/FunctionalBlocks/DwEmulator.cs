using System;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Prime;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.Forms;

namespace PrimeTutorial.Core.FunctionalBlocks
{
    class DwEmulator : IFunctionalBlock<Dwm, bool>
    {
        private readonly DwForm _form = new DwForm();

        private const int Delay = 100;
        private const double Base = 1;
        private const double Delta = 0.1;

        private readonly Matrix _matrix = new Matrix();
        private double _angle;

        public DwEmulator()
        {
            new Thread(() => Application.Run(_form)).Start();
        }

        public bool Process(Dwm input)
        {
            var time = 0d;

            while (time < input.Time)
            {
                CalculatePosition(input, time, Delta);

                time += Delta;
                Thread.Sleep(Delay);
            }

            CalculatePosition(input, input.Time, time - input.Time);
            Thread.Sleep(Delay);

            return true;
        }

        private void CalculatePosition(Dwm dwm, double time, double delta)
        {
            var vLeft = time * (dwm.VLeft1 - dwm.VLeft0) / dwm.Time + dwm.VLeft0;
            var vRight = time * (dwm.VRgiht1 - dwm.VRight0) / dwm.Time + dwm.VRight0;

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

            _form.SetPosition(Tuple.Create<double, double, double>(_matrix.OffsetX, _matrix.OffsetY, _angle));
        }
    }
}
