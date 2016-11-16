using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2;

namespace Task3.Tests
{
    class CustomQueueEqualityComparerByValues<T> : IEqualityComparer<CustomQueue<T>>
    {
        public bool Equals(CustomQueue<T> x, CustomQueue<T> y)
        {
            if (ReferenceEquals(x, null) && ReferenceEquals(y, null))
                return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Count == y.Count && x.All(item => y.Contains(item, EqualityComparer<T>.Default));
        }

        public int GetHashCode(CustomQueue<T> obj)
        {
            return obj.GetHashCode();
        }
    }
}
