using System;
using System.Linq;

namespace Generics
{
    /// <summary>
    /// A generic class that accepts only numeric types
    /// </summary>
    /// <typeparam name="T">A numeric(integral, float, double) type</typeparam>
    public class Number<T>
        where T : struct, IComparable<T>, IEquatable<T>, IFormattable, IConvertible
    {
        /// <summary>
        /// Array of acceptable generic types
        /// </summary>
        private static Type[] validTypes = new[]
        {
            typeof(int), typeof(uint), typeof(long),
            typeof(ulong), typeof(float), typeof(double),
            typeof(decimal), typeof(short), typeof(ushort),
            typeof(byte), typeof(sbyte), typeof(DateTime)
        };

        /// <summary>
        /// Sorts array in ascending order using Insertion Sort algorithm
        /// </summary>
        /// <param name="array">Array of numbers to be sorted</param>
        public static void InsertionSort(T[] array)
        {
            if (!IsTypeValid(typeof(T)))
                throw new ArgumentException($"{typeof(T)} is a not a valid Number type.");
            if (array.Length == 0 || array == null)
                throw new NullReferenceException("Null is not a valid argument type.");

            for(int i = 1; i < array.Length; i++)
            {
                T current = array[i];
                int idx = i - 1;
                while((idx >= 0) && array[idx].CompareTo(current) > 0)
                {
                    array[idx + 1] = array[idx];
                    idx--;
                }
                array[idx + 1] = current;
            }
        }

        /// <summary>
        /// Verifies if array elements are valid types
        /// </summary>
        /// <param name="t">Datatype to be tested for generic T constraints</param>
        /// <returns><c>true</c> if datatype is valid; otherwise <c>false</c></returns>
        private static bool IsTypeValid(Type t)
        {
            return validTypes.Contains(t);
        }
    }
}
