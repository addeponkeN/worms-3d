using System;

namespace Components
{
    public interface IExploder
    {
        public event Action<ExplodeData> ExplodeEvent;
    }
}