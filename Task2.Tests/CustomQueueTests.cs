using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task2;

namespace Task2.Tests
{
    [TestFixture]
    public class CustomQueueTests
    {
        private static readonly object[] sourceListsForCustomQueue_Enqueue =
        {
            new object[] { new List<int>{ 1, 2, 3, 4, 5},
                           new []{ 1, 2, 3, 4, 5} },

            new object[] { new List<int>{ 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946 },
                           new []{ 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946 } }
        };

        [Test,TestCaseSource(nameof(sourceListsForCustomQueue_Enqueue))]
        public void CustomQueue_TestForEnqueue(List<int> expectedResult, params int[] values)
        {
            var actual = new CustomQueue<int>();
            foreach (var value in values)
            {
                actual.Enqueue(value);
            }         

            Assert.AreEqual(expectedResult,new List<int>(actual));
        }

        private static readonly object[] sourceListsForCustomQueue_Dequeue =
        {
            new object[] { new List<int>{ 4, 5 },
                           new []{ 1, 2, 3, 4, 5} },

            new object[] { new List<int>{ 1597, 2584, 4181, 6765, 10946},
                           new []{ 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946 } }
        };

        [Test, TestCaseSource(nameof(sourceListsForCustomQueue_Dequeue))]
        public void CustomQueue_TestForDequeue(List<int> expectedResult, params int[] values)
        {
            var actual = new CustomQueue<int>();
            foreach (var value in values)
            {
                actual.Enqueue(value);
            }

            for (int i = 0; i < values.Length - expectedResult.Count; i++)
            {
                actual.Dequeue();
            }

            var a = new List<int>(actual);

            Assert.AreEqual(expectedResult, new List<int>(actual));
        }

        private static readonly object[] sourceListsForCustomQueue_CopyTo =
        {
            new object[] { new []{ 1, 2, 3, 4, 5} },

            new object[] { new []{ 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946 } }
        };

        [Test, TestCaseSource(nameof(sourceListsForCustomQueue_CopyTo))]
        public void CustomQueue_TestForCopyTo( params int[] expectedResult)
        {
            var queue = new CustomQueue<int>(expectedResult.ToList());

            var actual = new int[queue.Count];

            queue.CopyTo(actual,0);

            Assert.AreEqual(expectedResult, actual);
        }

        private static readonly object[] sourceListsForEnumiratorConstructo =
        {
            new object[] { new List<int>{ 1, 2, 3, 4, 5} } ,

            new object[] { new List<int>{ 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946 } }
        };

        [Test,TestCaseSource(nameof(sourceListsForEnumiratorConstructo))]
        public void CustomQueue_TestForEnumiratorConstructor(List<int> expectedResult) 
            => Assert.AreEqual(expectedResult, new List<int>(new CustomQueue<int>(expectedResult)));

       

    }
}
