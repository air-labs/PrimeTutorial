using System;
using Prime;
using PrimeTutorial.Core.Data;

namespace PrimeTutorial.Core.FunctionalBlocks
{
    public class DwmDriver : IFunctionalBlock<Geometry, Dwm[]>
    {
        private const double VMax = 20;
        private const double A = 10;
        private const double Base = 1;

        public Dwm[] Process(Geometry input)
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

        private Dwm[] ProcessLineGeometry(double value)
        {
            double time, v;
            int direction = Math.Sign(value);
            double l = Math.Abs(value);

            if (l > VMax * VMax / A)
            {
                time = VMax / A;
                v = direction * VMax;

                return new[]
                {
                    new Dwm(0, 0, v, v, time), 
                    new Dwm(v, v, v, v, (l - VMax * VMax / A) / VMax),
                    new Dwm(v, v,  0,  0, time)
                };
            }

            v = direction * Math.Sqrt(l * A);
            time = Math.Sqrt(l / A);

            return new[]
            {
                new Dwm(0, 0, v, v, time), 
                new Dwm(v, v, 0, 0, time)
            };
        }

        private Dwm[] ProcessSpotTurnGeometry(double value)
        {
            const double vMax = VMax / 10;
            const double w = 2 * vMax / Base;

            var time = Math.Abs(value / w);
            var v = Math.Sign(value) * vMax;

            return new [] { new Dwm(v, -v, v, -v, time) };
        }
    }
}
