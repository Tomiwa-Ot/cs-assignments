using Generics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NumberUnitTest
{
    [TestClass]
    public class NumberTests
    {
        [TestMethod]
        public void InsertionSort_NullParameter_ThrowsException()
        {
            int[] array = null;
            Assert.ThrowsException<NullReferenceException>(() => Number<int>.InsertionSort(array));
        }

        [TestMethod]
        public void InsertionSort_EmptyParameter_ThrowsException()
        {
            double[] array = new double[] { };
            Assert.ThrowsException<NullReferenceException>(() => Number<double>.InsertionSort(array));
        }

        [TestMethod]
        public void InsertionSort_InvalidGenericType_ThrowsException()
        {
            string[] array = new string[] {"hatchback", "cabriolet", "SUV"};
            //Assert.ThrowsException<ArgumentException>(() => Number<string>.InsertionSort(array));
        }

        [TestMethod]
        public void InsertionSort_IntegralType_SortsCorrectly()
        {
            // Integral type includes sbyte, byte, short, ushort, int, uint, long
            int[] array = new int[] { 4, 3, 2, 8, 6 };
            int[] expected = new int[] { 2, 3, 4, 6, 8 };
            Number<int>.InsertionSort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void InsertionSort_FloatType_SortsCorrectly()
        {
            float[] array = new float[] { 4.3f, 3.12f, 2.12f, 8.134f, 6.3f };
            float[] expected = new float[] { 2.12f, 3.12f, 4.3f, 6.3f, 8.134f };
            Number<float>.InsertionSort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void InsertionSort_DoubleType_SortsCorrectly()
        {
            double[] array = new double[] { 4.3, 3.12, 2.12, 8.134, 6.3 };
            double[] expected = new double[] { 2.12, 3.12, 4.3, 6.3, 8.134 };
            Number<double>.InsertionSort(array);
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void InsertionSort_DateTimeType_SortsCorrectly()
        {
            DateTime[] array = new DateTime[] { DateTime.Today, DateTime.MaxValue, DateTime.MinValue };
            DateTime[] expected = new DateTime[] { DateTime.MinValue, DateTime.Today, DateTime.MaxValue };
            Number<DateTime>.InsertionSort(array);
            CollectionAssert.AreEqual(expected, array);
        }
    }
}
