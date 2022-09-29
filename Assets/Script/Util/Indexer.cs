using UnityEngine;

namespace Util
{
    public struct Indexer
    {
        public static implicit operator Indexer(int length) => new(length);
        public static implicit operator int(Indexer ind) => ind.Current;
    
        public int Current => _current;
        public int Length;
    
        private int _current;
    
        private Indexer(int length)
        {
            Length = length;
            _current = 0;
        }

        public void SetCurrent(int i)
        {
            _current = Mathf.Clamp(i, 0, Length - 1);
        }
    
        public int Next()
        {
            return _current = (_current + 1) % Length;
        }
    }
}