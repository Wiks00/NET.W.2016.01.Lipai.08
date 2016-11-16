using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3;
using NUnit.Framework;
using Task2;

namespace Task3.Tests
{
    [TestFixture]
    public class CustomSetTests
    {
        private static readonly object[] sourceListsForCustomSet_Add =
        {
            new object[] { new List<string>{ "one", "1", " ", "five"},
                           new []{ "one", "1", " ", "one", "five" ," ","one" } }
        };

        [Test, TestCaseSource(nameof(sourceListsForCustomSet_Add))]
        public void CustomSet_TestForAdd(List<string> expectedValue, params string[] values)
        {
            var actual = new CustomSet<string>();
            foreach (var value in values)
            {
                actual.Add(value);
            }

            Assert.AreEqual(expectedValue, new List<string>(actual));
        }

        private static readonly object[] sourceListsForCustomSet_Remove =
        {
            new object[] { new List<string>{ "one", "1", "five"},
                           new []{ "one", "1", " ", "one", "five" ," ","one" } }
        };

        [Test, TestCaseSource(nameof(sourceListsForCustomSet_Remove))]
        public void CustomSet_TestForRemove(List<string> expectedValue, params string[] values)
        {
            var actual = new CustomSet<string>();
            foreach (var value in values)
            {
                actual.Add(value);
            }
            actual.Remove(" ");

            Assert.AreEqual(expectedValue, new List<string>(actual));
        }

        private static readonly object[] sourceListsForCustomSet_CopyTo =
        {
            new object[] { new []{ "one", "1", "EPAM", "five" } },

            new object[] { new []{ "1", "2", "3", "5", "8", "13", "21", "34", "55", "89", "144", "233", "377", "610", "987", "1597"} }
        };

        [Test, TestCaseSource(nameof(sourceListsForCustomSet_CopyTo))]
        public void CustomQueue_TestForCopyTo(params string[] expectedResult)
        {
            var set = new CustomSet<string>(expectedResult.ToList());

            var actual = new string[set.Count];

            Console.WriteLine(string.Join(" ",set));

            set.CopyTo(actual, 0);

            Console.WriteLine(string.Join(" ", actual.ToList()));

            Assert.AreEqual(expectedResult, actual);
        }

        private static readonly object[] sourceListsForCustomSet_SymmetricExceptWith =
        {
            new object[]
            {
                new CustomSet<string>{  "1", "EPAM", "five", "Microsoft", "string" },
                new List<string>{ "one", "1", "2" , "EPAM", "five" },
                new List<string>{ "Microsoft", "string",  "one", "2" },
            }
        };

        [Test, TestCaseSource(nameof(sourceListsForCustomSet_SymmetricExceptWith))]
        public void CustomQueue_TestForSymmetricExceptWith(CustomSet<string> actual, List<string> set2 , List<string> expectedResult)
        {

            actual.SymmetricExceptWith(set2);

            Console.WriteLine(string.Join(" ", actual));

            Assert.AreEqual(expectedResult, actual);
        }

        private static readonly object[] sourceListsForCustomSet_CustomEqualityComparer =
        {
            new object[]
            {
                new [] { 1, 2, 3, 4, 5 },
                1
            }
        };

        [Test, TestCaseSource(nameof(sourceListsForCustomSet_CustomEqualityComparer))]
        public void CustomQueue_TestForCustomEqualityComparer(int[] values, int expectedResult)
        {
            var list = new List<CustomQueue<int>> ();

            while (list.Count != 5)
            {
                var queue = new CustomQueue<int>(5);
                foreach (var value in values)
                {
                    queue.Enqueue(value);
                }
                list.Add(queue);
            }

            var set = new CustomSet<CustomQueue<int>>(list, new CustomQueueEqualityComparerByValues<int>());

            Assert.AreEqual(1, set.Count);
        }
    }
}
