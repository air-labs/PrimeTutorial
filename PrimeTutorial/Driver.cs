using Prime;
using System;
using System.Windows;
using PrimeTutorial.Core.Data;

namespace PrimeTutorial
{
    class Driver : IFunctionalBlock<ManipulatorPosition, ManipulatorAngles>
    {
        private bool isThrowsException;

        public Driver(bool isThrowsException = true)
        {
            this.isThrowsException = isThrowsException;
        }

        public ManipulatorAngles Process(ManipulatorPosition input)
        {
            var point3 = new Point(input.X, input.Y);
            
            var point2 = new Point(point3.X - Math.Cos(input.Alpha), point3.Y + Math.Sin(input.Alpha));

            var lengthSquared = new Vector(point2.X, point2.Y).LengthSquared;
            var h = Math.Sqrt(1 - lengthSquared / 4);

            if (Double.IsNaN(h) && isThrowsException)
            {
                throw new Exception("Target point is unreachable with given angle: " + input.ToString());
            }

            var length = Math.Sqrt(lengthSquared);
            var point1 = new Point(-h * point2.Y / length + point2.X / 2, h * point2.X / length + point2.Y / 2);

            var point0 = new Point(0, 0);

            return new ManipulatorAngles(
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
