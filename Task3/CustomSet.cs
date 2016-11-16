using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class CustomSet<T>: ISet<T> where T: class
    {
        #region private fields 
        private T[] structArray;
        private int realSize;
        private int currentInsertIndex;
        private readonly IEqualityComparer<T> equalityComparer;
        #endregion

        #region properties
        /// <summary>
        /// Gets the number of elements contained in the CustomSet
        /// </summary>
        public int Count => realSize;
        /// <summary>
        /// Gets a value indicating whether the CustomSet is read-only
        /// </summary>
        public bool IsReadOnly => structArray.IsReadOnly;
        #endregion


        #region Constructors
        /// <summary>
        /// Defult CustomSet counstructor
        /// </summary>
        public CustomSet(): this(EqualityComparer<T>.Default)
        {
        }

        /// <summary>
        /// Constructor that create CustomSet by given EqualityComparer comparer
        /// </summary>
        /// <param name="comparer">EqualityComparer object</param>
        public CustomSet(IEqualityComparer<T> comparer)
        {
            if (ReferenceEquals(comparer, null))
                comparer = EqualityComparer<T>.Default;

            structArray = new T[5];

            equalityComparer = comparer;
            realSize = 0;
            currentInsertIndex = 0;
        }


        /// <summary>
        /// Constructor that create CustomSet with given length
        /// </summary>
        /// <param name="length">length of created CustomSet</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public CustomSet(int length) : this(length, EqualityComparer<T>.Default)
        {
        }

        /// <summary>
        /// Constructor that create CustomSet with given length and EqualityComparer comparer
        /// </summary>
        /// <param name="length">length of created CustomSet</param>
        /// <param name="comparer">EqualityComparer object</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public CustomSet(int length, IEqualityComparer<T> comparer)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException();
            if (ReferenceEquals(comparer, null))
                comparer = EqualityComparer<T>.Default;

            structArray = new T[length];
            equalityComparer = comparer;
            realSize = 0;
            currentInsertIndex = 0;

        }

        /// <summary>
        /// Constructor that create CustomSet by given enumirable colletion
        /// </summary>
        /// <param name="collection">enumirable colletion</param>
        public CustomSet(IEnumerable<T> collection): this(collection,EqualityComparer<T>.Default)
        {
        }

        /// <summary>
        /// Constructor that create CustomSet by given enumirable colletion
        /// </summary>
        /// <param name="length">enumirable colletion</param>
        public CustomSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            if (ReferenceEquals(collection, null))
                throw new ArgumentNullException();
            if (ReferenceEquals(comparer, null))
                comparer = EqualityComparer<T>.Default;

            structArray = new T[collection.Count()];

            equalityComparer = comparer;
            realSize = 0;
            currentInsertIndex = 0;

            foreach (var item in collection)
            {
                Add(item);
            }
        }
        #endregion

        #region IEnumerable methods 
        /// <summary>
        /// Generate enumeration for CustomSet 
        /// </summary>
        /// <returns>enumeration of CustomSet</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region set methods
        #region main methods
        /// <summary>
        /// Add item into CustomSet
        /// </summary>
        /// <param name="item">added item</param>
        /// <returns>true if item succeeded added and false if differently</returns>
        public bool Add(T item)
        {
            if (Contains(item))
                return false;
            if (realSize == structArray.Length)
            {
                Array.Resize(ref structArray, structArray.Length * 2);
            }
            structArray[currentInsertIndex] = item;
            realSize++;
            currentInsertIndex++;
            return true;
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        /// <summary>
        /// Delete all items from CustomSet
        /// </summary>
        public void Clear()
        {
            Array.Clear(structArray, 0, realSize);
            currentInsertIndex = 0;
            realSize = 0;
        }

        /// <summary>
        /// Checks is an item exist in CustomSet
        /// </summary>
        /// <param name="item">Checked item</param>
        /// <returns>true if exist and false if differently</returns>
        public bool Contains(T item)
        {
            return this.Contains(item, equalityComparer);
        }

        /// <summary>
        /// Copy CustomSet into given array beginning from given index
        /// </summary>
        /// <param name="array">the array to be copied elements from the CustomSet</param>
        /// <param name="index">start index to be copied</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public void CopyTo(T[] array, int index)
        {
            if (index > realSize || index < 0 || array.Length < realSize - index)
                throw new ArgumentOutOfRangeException();
            if (ReferenceEquals(array, null))
                throw new ArgumentNullException();

            Array.Copy(structArray, index, array, 0, realSize - index);
        }

        /// <summary>
        /// Delete item in CustomSet
        /// </summary>
        /// <returns>true if deleted and false if differently</returns>
        /// <exception cref="ArgumentException"></exception>
        public bool Remove(T item)
        {
            if (realSize == 0 || !Contains(item))
                return false;

            for (var counter = Array.IndexOf(structArray, item); counter < Count; counter++)
            {
                structArray[counter] = structArray[counter + 1];
            }

            currentInsertIndex--;
            realSize--;

            return true;
        }
        #endregion

        #region collections association 
        /// <summary>
        /// Modifies the current set so that it contains all elements that are present in the current set, in the specified collection, or in both
        /// </summary>
        /// <param name="other">The collection to compare to the current set</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void UnionWith(IEnumerable<T> other)
        {
            if(ReferenceEquals(other,null))
                throw new ArgumentNullException();
            foreach (var item in other)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that are also in a specified collection
        /// </summary>
        /// <param name="other">The collection to compare to the current set</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void IntersectWith(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException();
            foreach (var item in other)
            {
                if(Contains(item))
                    Remove(item);
            }
        }

        /// <summary>
        /// Removes all elements in the specified collection from the current set
        /// </summary>
        /// <param name="other">The collection to compare to the current set</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException();
            foreach (var item in other)
            {
                Remove(item);
            }
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both
        /// </summary>
        /// <param name="other">The collection to compare to the current set</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException();
            foreach (var item in other)
            {
                if (Contains(item))
                    Remove(item);
                else
                    Add(item);
            }
        }
        #endregion

        #region check collections
        /// <summary>
        /// Determines whether a set is a subset of a specified collection
        /// </summary>
        /// <param name="other">The collection to compare to the current set</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>true if the current set is a subset of other. Otherwise, false</returns>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException();

            return other.Count() >= Count && other.All(Contains);
        }

        /// <summary>
        /// Determines whether the current set is a superset of a specified collection
        /// </summary>
        /// <param name="other">The collection to compare to the current set</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>true if the current set is a superset of other. Otherwise, false</returns>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException();

            return other.Count() <= Count && this.All(item => other.Contains(item, equalityComparer));
        }

        /// <summary>
        /// Determines whether the current set is a proper (strict) subset of a specified collection
        /// </summary>
        /// <param name="other">The collection to compare to the current set</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>true if the current set is a proper (strict) subset of other. Otherwise, false</returns>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException();

            return other.Count() > Count && other.All(Contains);
        }

        /// <summary>
        /// Determines whether the current set is a proper (strict) superset of a specified collection
        /// </summary>
        /// <param name="other">The collection to compare to the current set</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>true if the current set is a proper (strict) superset of other. Otherwise, false</returns>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException();

            return other.Count() < Count && this.All(item => other.Contains(item, equalityComparer));
        }

        /// <summary>
        /// Determines whether the current set overlaps with the specified collection
        /// </summary>
        /// <param name="other">The collection to compare to the current set</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>true if the current set and other share at least one common element. Otherwise, false</returns>
        public bool Overlaps(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException();

            return other.Any(Contains);
        }

        /// <summary>
        /// Determines whether the current set and the specified collection contain the same elements
        /// </summary>
        /// <param name="other">The collection to compare to the current set</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>true if the current set is equal to other. Otherwise, false</returns>
        public bool SetEquals(IEnumerable<T> other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException();

            return other.Count() == Count && other.All(Contains);
        }
        #endregion
        #endregion

        #region Enumerator for CustomQueue
        /// <summary>
        /// Custon Enumerator for CustomSet
        /// </summary>
        private struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            private CustomSet<T> set;
            private int index;

            /// <summary>
            /// return cuurent item of enumeration 
            /// </summary>
            public T Current
            {
                get
                {
                    if (index < 0)
                        throw new ArgumentOutOfRangeException();
                    return set.structArray[index];
                }
            }

            object IEnumerator.Current => Current;

            /// <summary>
            /// Constructor for creating Enumeration
            /// </summary>
            /// <param name="queue"></param>
            internal Enumerator(CustomSet<T> set)
            {
                this.set = set;
                index = -1;
            }

            /// <summary>
            /// Dispose all resources
            /// </summary>
            public void Dispose()
            {
                index = -1;
            }

            /// <summary>
            /// Step to the next item
            /// </summary>
            /// <returns>next item index</returns>
            public bool MoveNext()
            {
                index++;
                return index <= set.Count - 1;
            }

            /// <summary>
            /// Reset enumeration
            /// </summary>
            public void Reset()
            {
                index = -1;
            }
        }
        #endregion
    }
}
