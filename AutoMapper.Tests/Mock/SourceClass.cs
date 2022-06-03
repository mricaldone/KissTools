namespace KissTools.Tests.Mock
{
    internal class SourceClass
    {
        public string Name { get; set; }
        public double perimeter { get; set; }
        public double Distance_To_Sun { get; set; }
        public double Orbital_speed { get; set; }
        public double atmosfericPressure { get; set; }
        public string Gravity { get; set; }
        public string HasMoons { get; set; }
        public string Alias { get; set; }

        public bool SomeTask()
        {
            return false;
        }
    }
}
