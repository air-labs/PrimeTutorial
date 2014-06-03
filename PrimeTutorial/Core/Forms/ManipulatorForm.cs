using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using PrimeTutorial.Core.Data;
using Point = System.Windows.Point;

namespace PrimeTutorial.Core.Forms
{
    public partial class ManipulatorForm : Form
    {
        
        private readonly ConcurrentQueue<ManipulatorAngles> _queue = new ConcurrentQueue<ManipulatorAngles>();

        private bool _isWorking = true;

        private readonly Point[] _points =
        {
            new Point(0, 0),
            new Point(0, 1),
            new Point(1, 1),
            new Point(1, 0) 
        };

        private readonly int scale = 100;

        public ManipulatorForm()
        {
            InitializeComponent();

            var thread = new Thread(Algorithm)
            {
                IsBackground = true
            };

            thread.Start();
        }

        public void SetAngles(ManipulatorAngles angles)
        {
            _queue.Enqueue(angles);
        }

        private Point Translate(Point point)
        {
            return new Point(point.X * scale + ClientRectangle.Width / 2.0, ClientRectangle.Height - point.Y * scale);
        }

        private void EmulatorForm_Paint(object sender, PaintEventArgs e)
        {
            var graphics = CreateGraphics();
            graphics.Clear(Color.Azure);

            for (var i = 0; i < _points.Length - 1; i++)
            {
                var point1 = Translate(_points[i]);
                var point2 = Translate(_points[i + 1]);

                graphics.DrawLine(new Pen(Brushes.Tan, 3), (int) point1.X, (int) point1.Y, (int) point2.X, (int) point2.Y);
            }

            foreach (var origin in _points)
            {
                var point = Translate(origin);
                graphics.FillEllipse(Brushes.Maroon, (int) point.X - 5, (int) point.Y - 5, 10, 10);
            }
        }

        private void Algorithm()
        {
            while (_isWorking)
            {
                ManipulatorAngles angles;
                if (_queue.TryDequeue(out angles))
                {
                    _points[1].X = Math.Cos(angles.Betta1);
                    _points[1].Y = Math.Sin(angles.Betta1);

                    _points[2].X = _points[1].X - Math.Cos(angles.Betta1 + angles.Betta2);
                    _points[2].Y = _points[1].Y - Math.Sin(angles.Betta1 + angles.Betta2);

                    _points[3].X = _points[2].X + Math.Cos(angles.Betta1 + angles.Betta2 + angles.Betta3);
                    _points[3].Y = _points[2].Y + Math.Sin(angles.Betta1 + angles.Betta2 + angles.Betta3);

                    Refresh();
                }

                Thread.Sleep(100);
            }
        }

        private void EmulatorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isWorking = false;
        }

        private void EmulatorForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

    }
}
