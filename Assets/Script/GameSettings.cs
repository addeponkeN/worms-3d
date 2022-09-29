using System;

public static class GameSettings
{
    public static GameSetting<float> MasterVolume = new(0.5f);
    public static GameSetting<float> SfxVolume = new(1f);
    public static GameSetting<float> MusicVolume = new(1f);

}

public struct GameSetting<T>
{
    public event Action OnChangedEvent;
    private T _value;

    public T Value => _value;

    public GameSetting(T value)
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