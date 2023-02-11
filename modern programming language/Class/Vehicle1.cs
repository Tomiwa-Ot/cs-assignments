using System;

namespace Selection
{
    class Vehicle
    {
        // Fields
        private string name;
        private int topSpeed;

        // Constructor
        public Vehicle(string name, int topSpeed)
        {
            this.name = name;
            this.topSpeed = topSpeed;
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
