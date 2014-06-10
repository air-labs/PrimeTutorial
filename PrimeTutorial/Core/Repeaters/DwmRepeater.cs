using Prime;
using PrimeTutorial.Core.Data;

namespace PrimeTutorial.Core.Repeaters
{
    class DwmRepeater : IRepeaterBlock<Dwm[], bool, Dwm, bool>
    {
        private Dwm[] _dwms;
        private int _current;

        public void Start(Dwm[] publicIn)
        {
            _dwms = publicIn;
            _current = 0;
        }

        public bool MakeIteration(bool oldPrivateOut, out Dwm privateIn)
        {
            if (_current >= _dwms.Length)
            {
                privateIn = null;
                return false;
            }

            privateIn = _dwms[_current++];
            return true;
        }

        public bool Conclude()
        {
            return true;
        }
    }
}
