using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    public class CustomQueue<T> : IEnumerable<T>, ICollection
    {
        #region private fields 
        private T[] structArray;
        private int realSize;
        private int currentInsertIndex;
        private static object syncRoot;
        #endregion

        #region properties

        /// <summary>
        /// return length of CustomQueue
        /// </summary>
        public int Count => realSize;
        object ICollection.SyncRoot
        {
            get
            {
                if (ReferenceEquals(syncRoot, null))
                {
                    syncRoot = new object();
                }
                return syncRoot;
            }   
        }
        bool ICollection.IsSynchronized => false;

        int ICollection.Count => Count;
        #endregion

        #region Constructors
        /// <summary>
        /// Defult CustomQueue counstructor
        /// </summary>
        public CustomQueue()
        {
            structArray = new T[5];
            realSize = 0;
            currentInsertIndex = 0;
        }

        /// <summary>
        /// Constructor that create CustomQueue with given length
        /// </summary>
        /// <param name="length">length of created CustomQueue</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public CustomQueue(int length)
        {
            if(length <0)
                throw new ArgumentOutOfRangeException();

            structArray = new T[length];
            realSize = 0;
            currentInsertIndex = 0;

        }

        /// <summary>
        /// Constructor that create CustomQueue by given enumirable colletion
        /// </summary>
        /// <param name="collection">enumirable colletion</param>
        public CustomQueue(IEnumerable<T> collection)
        {
            if(ReferenceEquals(collection,null))
                throw new ArgumentNullException();

            structArray = new T[collection.Count()];

            realSize = 0;
            currentInsertIndex = 0;

            foreach (var item in collection)
            {
                Enqueue(item);   
            }
        }
        #endregion

        #region IEnumerable methods 
        /// <summary>
        /// Generate enumeration for CustomQueue 
        /// </summary>
        /// <returns>enumeration of CustomQueue</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region CustomQueue methods
        /// <summary>
        /// Copy CustomQueue into given array beginning from given index
        /// </summary>
        /// <param name="array">the array to be copied elements from the CustomQueue</param>
        /// <param name="index">start index to be copied</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public void CopyTo(Array array, int index)
        {
            if(index > realSize || index < 0 || array.Length < realSize - index)
                throw new ArgumentOutOfRangeException();
            if(ReferenceEquals(array,null))
                throw new ArgumentNullException();

            Array.Copy(structArray,index,array,0,realSize-index);
        }

        /// <summary>
        /// Delete all items from CustomQueue
        /// </summary>
        public void Clear()
        {
            Array.Clear(structArray,0,realSize);
            currentInsertIndex = 0;
            realSize = 0;
        }

        /// <summary>
        /// Generate hash code for CustomQueue
        /// </summary>
        /// <returns>CustomQueue hash code</returns>
        public override int GetHashCode()
        {
            var hashCode = 118 + this.Sum(value => value.GetHashCode() ^ 12);
            return hashCode*7;
        }

        /// <summary>
        /// Checks is an item exist in CustomQueue
        /// </summary>
        /// <param name="item">Checked item</param>
        /// <returns>true if exist and false if differently</returns>
        public bool Contains(T item)
        {
            return this.Contains(item,EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Return first item in CustomQueue without deleting  
        /// </summary>
        /// <returns>first item in CustomQueue</returns>
        /// <exception cref="ArgumentException"></exception>
        public T Peek()
        {
            if(realSize == 0)
                throw new ArgumentException();

            return structArray[currentInsertIndex - 1];
        }

        /// <summary>
        /// Return first item in CustomQueue with deleting  
        /// </summary>
        /// <returns>first item in CustomQueue</returns>
        /// <exception cref="ArgumentException"></exception>
        public T Dequeue()
        {
            if (realSize == 0)
                throw new ArgumentException();

            currentInsertIndex--;
            realSize--;

            var item = structArray[currentInsertIndex];
            structArray[currentInsertIndex] = default(T);

            return item;
        }

        /// <summary>
        /// Add item into CustomQueue
        /// </summary>
        /// <param name="item">added item</param>
        public void Enqueue(T item)
        {
            if (realSize == structArray.Length)
            {
                Array.Resize(ref structArray, structArray.Length *2);
            }
            structArray[currentInsertIndex] = item;
            realSize++;
            currentInsertIndex++;
        }
        #endregion

        #region Enumerator for CustomQueue
        /// <summary>
        /// Custon Enumerator for CustomQueue
        /// </summary>
        private struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            private CustomQueue<T> queue;
            private int index;

            /// <summary>
            /// return cuurent item of enumeration 
            /// </summary>
            public T Current
            {
                get
                {
                    if(index < 0)
                        throw new ArgumentOutOfRangeException();
                    return queue.structArray[index];
                }
            }

            object IEnumerator.Current => Current;

            /// <summary>
            /// Constructor for creating Enumeration
            /// </summary>
            /// <param name="queue"></param>
            internal Enumerator(CustomQueue<T> queue)
            {
                this.queue = queue;
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
                return index <= queue.Count -1;
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

