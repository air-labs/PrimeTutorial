using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Windows.Forms;
using PrimeTutorial.Core.FormsData;

namespace PrimeTutorial.Core.Forms
{
    public partial class ManipulatorForm : BaseForm
    {
        private readonly ConcurrentQueue<FormAngles> _queue = new ConcurrentQueue<FormAngles>();
        private FormAngles _angles = FormAngles.CreateDummyAngles(0, 0, 0);

        private readonly Pen _axisPen = new Pen(Color.Black, 0.01f);
        private readonly Pen _linkPen = new Pen(Color.Tan, 0.1f);
        private readonly Brush _jointBrush = Brushes.Maroon;

        public ManipulatorForm()
        {
            InitializeComponent();
        }

        public void SetAngles(FormAngles angles)
        {
            _queue.Enqueue(angles);
        }

        protected override void InitializeGraphics()
        {
            Graphics = CreateGraphics();
            TranslateGraphics(ClientRectangle.Width / 2.0, ClientRectangle.Height - 50);
            ScaleGraphics(100);
        }

        private void ManipulatorForm_Load(object sender, EventArgs e)
        {
            Text = "Manipulator Emulator";
            Size = new Size(700, 400);
        }

        private void ManipulatorForm_Paint(object sender, PaintEventArgs e)
        {
            FormAngles angles;
            if (_queue.TryDequeue(out angles))
            {
                _angles = angles;
            }

            DrawLine(_axisPen, 0, 3, 0, -0.3);
            DrawLine(_axisPen, 3, 0, -3, 0);

            PushGraphics();

            RotateGraphics(_angles.Beta1);
            DrawLine(_linkPen, 0, 0, 1, 0);

            TranslateGraphics(1, 0);
            RotateGraphics(Math.PI + _angles.Beta2);

            DrawLine(_linkPen, 0, 0, 1, 0);
            DrawPoint(_jointBrush, 0, 0, 0.2);

            TranslateGraphics(1, 0);
            RotateGraphics(Math.PI + _angles.Beta3);

            DrawLine(_linkPen, 0, 0, 1, 0);
            DrawPoint(_jointBrush, 0, 0, 0.2);
            DrawPoint(_jointBrush, 1, 0, 0.2);

            PopGraphics();

            DrawPoint(_jointBrush, 0, 0, 0.2);
        }

    }
}
