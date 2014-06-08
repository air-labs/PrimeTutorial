using System.Threading;
using System.Windows.Forms;
using Prime;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.Forms;
using PrimeTutorial.Core.FormsData;

namespace PrimeTutorial.Core.FunctionalBlocks
{
    public class ManipulatorEmulator : IFunctionalBlock<ManipulatorAngles, bool>
    {
        private readonly ManipulatorForm _form = new ManipulatorForm();
        private ManipulatorAngles _angles = new ManipulatorAngles(1.8, 1.5, 1.4);

        private const double Step = 0.1;
        private const int Delay = 100;

        public ManipulatorEmulator()
        {
            _form.SetAngles(FormAngles.CreateDummyAngles(_angles));
            new Thread(() => Application.Run(_form)).Start();
        }

        public bool Process(ManipulatorAngles input)
        {
            var formAngles = new FormAngles(_angles, input, Step);
            while (formAngles.IsMove())
            {
                formAngles.MakeStep();

                _form.SetAngles(formAngles);
                Thread.Sleep(Delay);
            }

            _form.SetAngles(FormAngles.CreateDummyAngles(input));
            Thread.Sleep(Delay);

            _angles = input;
            return true;
        }
    }
}
