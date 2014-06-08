using System;
using PrimeTutorial.Core.Data;

namespace PrimeTutorial.Core.FormsData
{
    public class FormAngles
    {
        public double Beta1 { get; private set; }
        public double Beta2 { get; private set; }
        public double Beta3 { get; private set; }

        private readonly ManipulatorAngles _destination;

        private readonly double _step;

        public static FormAngles CreateDummyAngles(ManipulatorAngles destination)
        {
            return new FormAngles(destination, destination, Math.PI);
        }

        public static FormAngles CreateDummyAngles(double beta1, double beta2, double beta3)
        {
            var destination = new ManipulatorAngles(beta1, beta2, beta3);
            return CreateDummyAngles(destination);
        }

        public FormAngles(ManipulatorAngles source, ManipulatorAngles destination, double step)
        {
            Beta1 = source.Betta1;
            Beta2 = source.Betta2;
            Beta3 = source.Betta3;

            _destination = destination;
            _step = step;
        }

        public bool IsMove()
        {
            return HasNextStep(Beta1, _destination.Betta1) 
                || HasNextStep(Beta2, _destination.Betta2)
                || HasNextStep(Beta3, _destination.Betta3);
        }

        public void MakeStep()
        {
            Beta1 = NextStep(Beta1, _destination.Betta1);
            Beta2 = NextStep(Beta2, _destination.Betta2);
            Beta3 = NextStep(Beta3, _destination.Betta3);
        }

        private bool HasNextStep(double source, double destination)
        {
            return Math.Abs(destination - source) > _step;
        }

        private double NextStep(double source, double destination)
        {
            return HasNextStep(source, destination) ? source + Math.Sign(destination - source) * _step : destination;
        }
    }
}
