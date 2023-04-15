using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[] { 7, 5, -1, 12, 1, 4, 0, -33};
            Console.WriteLine($"Not sorted: {String.Join(", ", array)}");
            Number<int>.InsertionSort(array);
            Console.WriteLine($"Sorted: {String.Join(", ", array)}");
        }
    }
}
