using System;
using System.Threading;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using Prime;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.Forms;

namespace PrimeTutorial.Core.FunctionalBlocks
{
    public class ManipulatorEmulator : IFunctionalBlock<ManipulatorAngles, bool>
    {
        private readonly ManipulatorForm _form = new ManipulatorForm();
        private ManipulatorAngles _angles = new ManipulatorAngles(1.8, 1.5, 1.4);

        private const double Delta = 0.1;
        private const int Delay = 100;

        public ManipulatorEmulator()
        {
            _form.SetAngles(_angles);
            new Thread(() => Application.Run(_form)).Start();
        }

        public bool Process(ManipulatorAngles input)
        {
            var sign1 = Math.Sign(input.Betta1 - _angles.Betta1);
            var beta1 = _angles.Betta1;

            var sign2 = Math.Sign(input.Betta2 - _angles.Betta2);
            var beta2 = _angles.Betta2;

            var sign3 = Math.Sign(input.Betta3 - _angles.Betta3);
            var beta3 = _angles.Betta3;

            while (true)
            {
                var isBeta1Move = NextStep(ref beta1, sign1 * Delta, input.Betta1);
                var isBeta2Move = NextStep(ref beta2, sign2 * Delta, input.Betta2);
                var isBeta3Move = NextStep(ref beta3, sign3 * Delta, input.Betta3);

                _form.SetAngles(new ManipulatorAngles(beta1, beta2, beta3));
                Thread.Sleep(Delay);

                if (!isBeta1Move && !isBeta2Move && !isBeta3Move)
                {
                    break;
                }
            }
            
            _angles = input;
            return true;
        }

        private bool NextStep(ref double angle, double delta, double target)
        {
            if (Math.Abs(angle - target) <= Math.Abs(delta))
            {
                angle = target;
                return false;
            }

            angle += delta;
            return true;
        }
    }
}
