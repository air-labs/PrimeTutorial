using System.Threading;
using System.Windows.Forms;
using Prime;
using PrimeTutorial.Core.Data;
using PrimeTutorial.Core.Forms;

namespace PrimeTutorial.Core.FunctionalBlocks
{
    class DwEmulator : IFunctionalBlock<Dwm, bool>
    {
        private readonly DwForm _form = new DwForm();

        public DwEmulator()
        {
            new Thread(() => Application.Run(_form)).Start();
        }

        public bool Process(Dwm input)
        {
            _form.PutDwm(input);
            return true;
        }
    }
}
