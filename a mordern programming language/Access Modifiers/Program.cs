using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessModifiers
{
    /// <summary>
    /// Olorunfemi-Ojo Daniel Tomiwa 190805503
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    abstract class Vehicle
    {
        // Abstract method implementation occurs in sub class
        public abstract double TopSpeedInKilometresPerHour(double speed);
    }

    class Car : Vehicle
    {
        public override double TopSpeedInKilometresPerHour(double speedInKilometresPerHour)
        {
            return speedInKilometresPerHour;
        }

        // Protected method only accessible from sub class
        protected double ConvertSpeedToMetresPerSecond(double speedInKilometresPerHour)
        {
            return speedInKilometresPerHour * (5.0 / 18.0);
        }

        // Virtual allows extra functionalities to be added to the method
        public virtual void DisplayCarStatistics()
        {
            Console.WriteLine("This vehicle has the following stats: ");
        }
    }

    class SUV : Car
    {
        // Private: only accessible within this class
        public int horsePower { get; }
        private string engineType;
        private double speedInMetresPerSecond;

        public SUV(int horsePower, string engineType, double speedInMetresPerSecond)
        {
            this.horsePower = horsePower;
            this.engineType = engineType;
            // Calling protected method
            this.speedInMetresPerSecond = ConvertSpeedToMetresPerSecond(speedInMetresPerSecond);
        }

        // Cannot be accessed via an object of this class
        public static void FullMeaning()
        {
            Console.WriteLine("SUV means Sport Utility Vehicle");
        }

        // Override adds extra functionalities to inherited method
        public override void DisplayCarStatistics()
        {
            base.DisplayCarStatistics();
            Console.WriteLine($"Horse Power: {horsePower}");
            Console.WriteLine($"Engine Type: {engineType}");
            Console.WriteLine($"Speed: {speedInMetresPerSecond}m/s");
        }
    }

    internal class Log
    {
        public static void WriteToLog(string data)
        {
            Console.WriteLine("Successfully added to logs");
        }
    }

    // Partial keyword allows multiple declarations of the same class and sums up
    // the fields, methods and properties as one class
    partial class VehicleInspection
    {
        public static bool RoadLegal(SUV suv)
        {
            return suv.horsePower > 1000;
        }
    }

    partial class VehicleInspection
    {
        public static void LogData(SUV suv)
        {
            if (VehicleInspection.RoadLegal(suv))
            {
                Log.WriteToLog("This SUV is road legal");
            }
            else
            {
                Log.WriteToLog("This SUV is not road legal");
            }
        }
    }
}
