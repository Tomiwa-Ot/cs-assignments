using System;

namespace Selection
{
    class Vehicle
    {
        // Constructor
        public Vehicle(string name, int topSpeed)
        {
            Console.WriteLine($"{name} has a top speed of {topSpeed}km/h")
        }

        // Method
        public double SpeedInMetresPerSecond(int speed)
        {
            return speed * (5.0/18.0);
        }

        // Properties
        public double MilesPerGallon { get; set; }
        public double Cylinders { get; set; }
    }
}
