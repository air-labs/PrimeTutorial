using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Drawing2D;
using PrimeTutorial.Core.Data;

namespace PrimeTutorial.Core.Forms
{
    public partial class DwForm : BaseForm
    {
        private readonly ConcurrentQueue<Dwm> _queue = new ConcurrentQueue<Dwm>();
        private readonly ConcurrentQueue<Tuple<double, double, double>> _points = new ConcurrentQueue<Tuple<double, double, double>>();

        private Matrix matrix = new Matrix();

        public DwForm()
        {
            InitializeComponent();
        }

        public void PutDwm(Dwm dwm)
        {
            _queue.Enqueue(dwm);
        }

        protected override void InitializeGraphics()
        {
            base.InitializeGraphics();
            TranslateGraphics(ClientRectangle.Width / 2.0, ClientRectangle.Height / 2.0);
            ScaleGraphics(25);
        }

        private void DwForm_Load(object sender, EventArgs e)
        {
            Text = "Double Wheel Emulator";
            Size = new Size(800, 600);
        }

        private void DwForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            DrawLine(AxisPen, 0, 10, 0, -10);
            DrawLine(AxisPen, 10, 0, -10, 0);

            Calculate();

            PushGraphics();

            Graphics.MultiplyTransform(matrix);
            DrawRectangle(Brushes.MediumAquamarine, 0, 0, 2, 1);
            
            PopGraphics();
        }

        private void Calculate()
        {
            Dwm dwm;
            if (_points.IsEmpty && !_queue.IsEmpty && _queue.TryDequeue(out dwm))
            {
                double vl = dwm.VLeft1;
                double vr = dwm.VRgiht1;

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
                var steps = (int)dwm.Time / time;
                for (var i = 0; i < steps; i++)
                {
                    double angle = -w * time;
                    double y = Math.Cos(angle) * offset * time;
                    double x = Math.Sin(angle) * offset * time;

                    _points.Enqueue(Tuple.Create(x, y, angle * 180 / Math.PI));
                }
            }

            Tuple<double, double, double> point;
            if (!_points.IsEmpty && _points.TryDequeue(out point))
            {
                matrix.Rotate((float)point.Item3);
                matrix.Translate((float)point.Item1, (float)point.Item2);
            }
        }
    }
}
