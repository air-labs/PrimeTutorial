using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PrimeTutorial.Core.Data;

namespace PrimeTutorial.Core.Forms
{
    public partial class DwForm : Form
    {
        private readonly ConcurrentQueue<Dwm> _queue = new ConcurrentQueue<Dwm>();
        private readonly ConcurrentQueue<Tuple<double, double, double>> _points = new ConcurrentQueue<Tuple<double, double, double>>(); 

        private readonly Timer _timer = new Timer();

        private Graphics _graphics;

        private Matrix matrix = new Matrix();

        public DwForm()
        {
            InitializeComponent();
        }

        public void PutDwm(Dwm dwm)
        {
            _queue.Enqueue(dwm);
        }

        private void Calculate(object sender, EventArgs e)
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
                var steps = (int) dwm.Time/time;
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

                Refresh();
            }
        }

        private void DwForm_Load(object sender, EventArgs e)
        {

            _timer.Tick += Calculate;
            _timer.Interval = 100;
            _timer.Enabled = true;
            _timer.Start();
        }

        private void DwForm_Paint(object sender, PaintEventArgs e)
        {
            _graphics = CreateGraphics();
            _graphics.ResetTransform();

            _graphics.TranslateTransform((float)(ClientRectangle.Width / 2.0), (float)(ClientRectangle.Height / 2.0));
            _graphics.ScaleTransform((float)scale, (float)-scale);

            _graphics.Clear(Color.Azure);

            _graphics.DrawLine(new Pen(Color.Black, (float) 0.01), 0, 10, 0, -10);
            _graphics.DrawLine(new Pen(Color.Black, (float) 0.01), 10, 0, -10, 0);

            _graphics.MultiplyTransform(matrix);
            DrawRectangle(0, 0, 2, 1);
        }

        private void DrawPoint(double x, double y, double radius)
        {
            _graphics.FillEllipse(Brushes.Maroon, (float) (x - radius / 2), (float) (y - radius / 2), (float) radius, (float) radius);
        }

        private void DrawRectangle(double x, double y, double width, double height)
        {
            _graphics.FillRectangle(Brushes.MediumAquamarine, (float) (x - width / 2), (float) (y - height / 2), (float) width, (float) height);
        }

        private void DwForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
