using System;
using PrimeTutorial.Core.Data;

namespace PrimeTutorial.Core.FormsData
{
    class FormAngles
    {
        private double _beta1;
        private double _beta2;
        private double _beta3;

        private readonly ManipulatorAngles _destination;

        private readonly double _step;

        public FormAngles(ManipulatorAngles source, ManipulatorAngles destination, double step)
        {
            _beta1 = source.Betta1;
            _beta2 = source.Betta2;
            _beta3 = source.Betta3;

            _destination = destination;
            _step = step;
        }

        public bool IsMove()
        {
            return HasNextStep(_beta1, _destination.Betta1) 
                || HasNextStep(_beta2, _destination.Betta2)
                || HasNextStep(_beta3, _destination.Betta3);
        }

        public void MakeStep()
        {
            _beta1 = NextStep(_beta1, _destination.Betta1);
            _beta2 = NextStep(_beta2, _destination.Betta2);
            _beta3 = NextStep(_beta3, _destination.Betta3);
        }

        public ManipulatorAngles ToManipulatorAngles()
        {
            return new ManipulatorAngles(_beta1, _beta2, _beta3);
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
