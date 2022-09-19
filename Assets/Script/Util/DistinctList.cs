using System.Collections.Generic;

namespace Util
{
    public class DistinctList<T>
    {
        public T this[int index] => Items[index];
        
        public int Count => Items.Count;

        public List<T> Items;

        private HashSet<T> _hash;

        public DistinctList()
        {
            Items = new();
            _hash = new();
        }

        public bool Add(T t)
        {
            if(_hash.Contains(t))
                return false;
            Items.Add(t);
            _hash.Add(t);
            return true;
        }

        public void Remove(T t)
        {
            Items.Remove(t);
            _hash.Remove(t);
        }

        public void Clear()
        {
            Items.Clear();
            _hash.Clear();
        }

        public void RemoveAt(int i)
        {
            var item = Items[i];
            _hash.Remove(item);
            Items.RemoveAt(i);
        }
    }
}