using System;

namespace Settings
{
    public struct GameSetting<T>
    {
        public string Name { get; private set; }
        public T Value { get; private set; }

        public event Action OnChangedEvent;

        public GameSetting(string name, T value)
        {
            Name = name;
            Value = value;
            OnChangedEvent = null;
        }

        public void SetValue(T value)
        {
            Value = value;
            OnChangedEvent?.Invoke();
        }
        
    }
}