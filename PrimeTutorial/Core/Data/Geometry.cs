using System;

namespace PrimeTutorial.Core.Data
{
    [Serializable]
    public class Geometry
    {
        public GeometryType Type { get; private set; }

        public double Value { get; private set; }

        public static Geometry CreateLineGeometry(double value)
        {
            return new Geometry(GeometryType.Line, value);
        }

        public static Geometry CreateSpotTurnGeometry(double value)
        {
            return new Geometry(GeometryType.SpotTurn, value);
        }

        private Geometry(GeometryType type, double value)
        {
            Type = type;
            Value = value;
        }
    }
}
