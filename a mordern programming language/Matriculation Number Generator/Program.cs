using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculationNumberSimulator
{
    class Program
    {
        static SchoolPortal portal;

        static void Main(string[] args)
        {
            portal = new SchoolPortal();
            MainMenu();
        }

        static void MainMenu()
        {
            Console.Clear();
            int option = 0;
            Console.WriteLine("1. Create student\n2. List students\n3. Exit\n");
            Console.Write("> : ");
            if(int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        portal.CreateStudent();
                        break;
                    case 2:
                        portal.ListStudents();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        MainMenu();
                        break;
                }
            }
            MainMenu();
        }
    }
}
