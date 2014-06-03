using System.Threading;
using System.Windows.Forms;
using Prime;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.Forms;

namespace PrimeTutorial.Core.FunctionalBlocks
{
    public class ManipulatorEmulator : IFunctionalBlock<ManipulatorAngles, bool>
    {
        private readonly ManipulatorForm _form = new ManipulatorForm();

        public ManipulatorEmulator()
        {
            new Thread(() => Application.Run(_form)).Start();
        }

        public bool Process(ManipulatorAngles input)
        {
            _form.SetAngles(input);
            return true;
        }
    }
}
