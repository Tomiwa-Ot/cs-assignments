using System;

namespace Selection
{
    class Program
    {
        static void Main(string[] args)
        {
            int score = 35;
            if(score < 40)
            {
                Console.WriteLine("Fail");
            }

            int number = 5;
            if (number % 2 == 0)
            {
                Console.WriteLine("{0} is an even number.", number);
            } else
            {
                Console.WriteLine("{0} is an odd number.", number);
            }

            String countryCode = "+44";
            if (countryCode.Equals("+234"))
            {
                Console.WriteLine("Nigeria");
            } else if (countryCode.Equals("+44"))
            {
                Console.WriteLine("United Kingdom");
            } else
            {
                Console.WriteLine("Unknown");
            }

            switch(countryCode)
            {
                case "+234":
                    Console.WriteLine("Nigeria");
                    break;
                case "+44":
                    Console.WriteLine("United Kingdom");
                    break;
                default:
                    Console.WriteLine("Unknown");
                    break;
            }
        }
    }
}
