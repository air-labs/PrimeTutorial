using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PrimeTutorial.Core.Forms
{
    public partial class BaseForm : Form
    {
        protected Graphics Graphics;
        private readonly Stack<GraphicsState> _graphicsStates = new Stack<GraphicsState>();

        protected readonly Pen AxisPen = new Pen(Color.Black, 0.01f);

        protected Timer Timer;

        protected BaseForm()
        {
            InitializeComponent();
        }

        protected virtual void InitializeGraphics()
        {
            Graphics = CreateGraphics();
        }

        protected virtual void InitializeTimer()
        {
            Timer = new Timer
            {
                Enabled = true,
                Interval = 100
            };

            Timer.Tick += timer_Tick;
            Timer.Start();
        }

        protected void PushGraphics()
        {
            _graphicsStates.Push(Graphics.Save());
        }

        protected void PopGraphics()
        {
            Graphics.Restore(_graphicsStates.Pop());
        }

        protected void DrawPoint(Brush brush, double x, double y, double radius)
        {
            Graphics.FillEllipse(brush, (float) (x - radius / 2), (float) (y - radius / 2), (float) radius, (float) radius);
        }

        protected void DrawLine(Pen pen, double x1, double y1, double x2, double y2)
        {
            Graphics.DrawLine(pen, (float) x1, (float) y1, (float) x2, (float) y2);
        }

        protected void DrawRectangle(Brush brush, double x, double y, double width, double height)
        {
            Graphics.FillRectangle(brush, (float) (x - width / 2), (float) (y - height / 2), (float) width, (float) height);
        }

        protected void TranslateGraphics(double x, double y)
        {
            Graphics.TranslateTransform((float)x, (float)y);
        }

        protected void RotateGraphics(double angle)
        {
            Graphics.RotateTransform((float) (angle * 180 / Math.PI));
        }

        protected void ScaleGraphics(double scale)
        {
            Graphics.ScaleTransform((float)scale, (float)-scale);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            InitializeGraphics();
            InitializeTimer();
        }

        private void BaseForm_Resize(object sender, EventArgs e)
        {
            InitializeGraphics();
            Refresh();
        }
    }
}
