namespace PrimeTutorial.Core.FormsData
{
    public class FormDwPosition
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public double Angle { get; private set; }

        public FormDwPosition(double x, double y, double angle)
        {
            X = x;
            Y = y;
            Angle = angle;
        }
    }
}
