using System;

public static class GameSettings
{
    public static GameSettings<float> MasterVolume = new(0.5f);
    public static GameSettings<float> SfxVolume = new(1f);
    public static GameSettings<float> MusicVolume = new(1f);

}


public struct GameSettings<T>
{
    public event Action OnChangedEvent;
    private T _value;

    public T Value => _value;

    public GameSettings(T value)
    {
        _value = value;
        OnChangedEvent = null;
    }

    public void SetValue(T value)
    {
        _value = value;
        OnChangedEvent?.Invoke();
    }
}