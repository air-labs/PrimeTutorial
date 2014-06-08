using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Drawing2D;
using PrimeTutorial.Core.Data;

namespace PrimeTutorial.Core.Forms
{
    public partial class DwForm : BaseForm
    {
        private readonly ConcurrentQueue<Tuple<double, double, double>> _points = new ConcurrentQueue<Tuple<double, double, double>>();

        private Matrix matrix = new Matrix();

        public DwForm()
        {
            InitializeComponent();
        }

        public void PutDwm(Tuple<double, double, double> point)
        {
            _points.Enqueue(point);
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

            Tuple<double, double, double> point;
            if (!_points.IsEmpty && _points.TryDequeue(out point))
            {
                matrix.Rotate((float) point.Item3);
                matrix.Translate((float) point.Item1, (float) point.Item2);
            }

            PushGraphics();

            Graphics.MultiplyTransform(matrix);
            DrawRectangle(Brushes.MediumAquamarine, 0, 0, 2, 1);
            
            PopGraphics();
        }
    }
}
