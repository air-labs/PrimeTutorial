using System;
using Prime;
using PrimeTutorial.Core.Data;

namespace PrimeTutorial.Core.FunctionalBlocks
{
    public class SimpleDwmDriver : IFunctionalBlock<Geometry, Dwm>
    {

        private const double VMax = 20;
        private const double Base = 1;

        public Dwm Process(Geometry input)
        {
            switch (input.Type)
            {
                case GeometryType.Line:
                    return ProcessLineGeometry(input.Value);

                case GeometryType.SpotTurn:
                    return ProcessSpotTurnGeometry(input.Value);
            }

            throw new ArgumentException("Unknown geometry type: " + input.Type);
        }

        private Dwm ProcessLineGeometry(double value)
        {
            return new Dwm(VMax, VMax, VMax, VMax, value / VMax);
        }

        private Dwm ProcessSpotTurnGeometry(double value)
        {
            const double w = 2 * VMax / Base;

            var time = Math.Abs(value / w);
            var v = Math.Sign(value) * VMax;

            return new Dwm(v, -v, v, -v, time);
        }
    }
}
