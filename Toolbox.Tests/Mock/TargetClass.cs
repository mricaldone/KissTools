namespace Toolbox.Tests.Mock
{
    internal class TargetClass
    {
        public string Name { get; set; }
        public double Perimeter { get; set; }
        public double DistanceToSun { get; set; }
        public double OrbitalSpeed { get; set; }
        public double Pressure { get; set; }
        public double Gravity { get; set; }
        public bool HasMoons { get; set; }
        public bool WithMoons { get; set; }

        public bool SomeTask()
        {
            return true;
        }

    }
}
