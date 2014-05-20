using Prime;
using System;
using System.Windows;

namespace PrimeTutorial
{
    class Driver : IFunctionalBlock<Tuple<double, double, double>, Tuple<double, double, double>>
    {
        private bool throwsException;

        public Driver() : this(true)
        {
        }

        public Driver(bool throwsException) : base()
        {
            this.throwsException = throwsException;
        }

        public Tuple<double, double, double> Process(Tuple<double, double, double> input)
        {
            var point3 = new Point(input.Item1, input.Item2);
            
            var point2 = new Point(point3.X - Math.Cos(input.Item3), point3.Y + Math.Sin(input.Item3));

            var lengthSquared = new Vector(point2.X, point2.Y).LengthSquared;
            var h = Math.Sqrt(1 - lengthSquared / 4);

            if (Double.IsNaN(h) && throwsException)
            {
                throw new Exception("Target point is unreachable with given angle: " + input.ToString());
            }

            var length = Math.Sqrt(lengthSquared);
            var point1 = new Point(-h * point2.Y / length + point2.X / 2, h * point2.X / length + point2.Y / 2);

            var point0 = new Point(0, 0);

            return Tuple.Create(
                AngleBetweenVectors(new Vector(1.0, 0), CreateVector(point1, point0)),
                AngleBetweenVectors(CreateVector(point0, point1), CreateVector(point2, point1)),
                AngleBetweenVectors(CreateVector(point1, point2), CreateVector(point3, point2)));
        }

        private Vector CreateVector(Point point1, Point point2)
        {
            var point = point2 - point1;
            return new Vector(point.X, point.Y);
        }

        private double AngleBetweenVectors(Vector v, Vector u)
        {
            return Math.Acos(v.X * u.X + v.Y * u.Y / (v.Length * u.Length));
        }
    }
}
