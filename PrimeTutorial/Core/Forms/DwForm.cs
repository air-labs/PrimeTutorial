using System;
using System.Collections.Concurrent;
using System.Drawing;
using PrimeTutorial.Core.FormsData;

namespace PrimeTutorial.Core.Forms
{
    public partial class DwForm : BaseForm
    {
        private readonly ConcurrentQueue<FormDwPosition> _queue = new ConcurrentQueue<FormDwPosition>();

        private FormDwPosition _position = new FormDwPosition(0, 0, 0);

        public DwForm()
        {
            InitializeComponent();
        }

        public void SetPosition(FormDwPosition position)
        {
            _queue.Enqueue(position);
        }

        protected override void InitializeGraphics()
        {
            base.InitializeGraphics();
            TranslateGraphics(ClientRectangle.Width / 2.0, ClientRectangle.Height / 2.0);
            ScaleGraphics(10);
        }

        private void DwForm_Load(object sender, EventArgs e)
        {
            Text = "Double Wheel Emulator";
            Size = new Size(800, 600);
        }

        private void DwForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            DrawLine(AxisPen, 0, 25, 0, -25);
            DrawLine(AxisPen, 25, 0, -25, 0);

            PushGraphics();

            FormDwPosition position;
            if (!_queue.IsEmpty && _queue.TryDequeue(out position))
            {
                _position = position;
            }

            TranslateGraphics(_position.X, _position.Y);
            RotateGraphics(_position.Angle);
            
            DrawRectangle(Brushes.MediumAquamarine, 0, 0, 2, 1);
            
            PopGraphics();
        }
    }
}
