using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsComposition.Common
{
    public class Collection<T> : ICollection<T>
    {
        private List<T> _items;

        public Collection()
        {
            _items = new List<T>();
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public T this[int i]
        {
            get
            {
                return _items[i];
            }

            set
            {
                _items[i] = value;
            }
        }

        public void Add(T p)
        {
            if (!_items.Contains(p))
            {
                _items.Add(p);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("The array cannot be null.");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            if (Count > array.Length - arrayIndex + 1)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            for (int i = 0; i < _items.Count; i++)
            {
                array[i + arrayIndex] = _items[i];
            }
        }

        public bool Remove(T p)
        {
            return _items.Remove(p);
        }

        public bool Contains(T p1)
        {
            foreach (T p in _items)
            {
                if (p.Equals(p1))
                {
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _items[i];
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
