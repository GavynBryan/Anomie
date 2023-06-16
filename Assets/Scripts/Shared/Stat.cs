using UnityEngine;


[System.Serializable]
public class Stat
{
    public int value;
    public int maxValue;

    public Stat(int v, int mv)
    {
        value = v;
        maxValue = mv;
    }

    public static Stat operator+(Stat a, int b)
    {
        a.value = Mathf.Clamp(a.value + b, 0, a.maxValue);
        return a;
    }

    public static Stat operator -(Stat a, int b)
    {
        a.value = Mathf.Clamp(a.value - b, 0, a.maxValue);
        return a;
    }
}
