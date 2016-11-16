using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static Task1.FibonacciEnumerator;

namespace Task1.Tests
{
    [TestFixture]
    public class FibonacciEnumeratorTests
    {

        private static readonly object[] sourceListsForFibonacciEnumerator =
        {
            new object[] {1, new List<int>{ 1 } },
            new object[] {20, new List<int>{ 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946 } },
            new object[] {4, new List<int>{  1, 2, 3, 5 } },
            new object[] {15, new List<int>{ 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987 } }
        };

        [Test, TestCaseSource(nameof(sourceListsForFibonacciEnumerator))]
        public void CreateFibonacciEnumerator_TheCreationFibonacciEnumeratorOfAGivenDimension(int dimension, List<int> expectedResult) =>Assert.AreEqual(expectedResult, new List<int>(CreateFibonacciEnumerator(dimension)));
  
    }
}
