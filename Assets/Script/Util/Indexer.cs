namespace Util
{
    public struct Indexer
    {
        public int Value;
        private int _length;

        public Indexer(int length)
        {
            _length = length;
            Value = 0;
        }
        
        public int Next()
        {
            return Value = (Value + 1) % _length;
        }

        public static implicit operator int(Indexer ind) => ind.Value;
        public static implicit operator Indexer(int length) => new Indexer() {_length = length};
    }
}