public struct Life
{
    public int Value;

    public Life(int value)
    {
        Value = value;
    }

    public static implicit operator int(Life l) => l.Value;
    public static implicit operator bool(Life l) => l.Value > 0;
}