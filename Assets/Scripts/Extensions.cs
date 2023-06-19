using Unity.VisualScripting;
using UnityEngine;

public static class Extensions
{
    public static Vector3 Flatten(this Vector3 v)
    {
        return new Vector3(v.x, 0, v.z);
    }

    public static float WrapToAngle(this float f)
    {
        f = f % 360f; 
        if (f < 0f) {
            f += 360f; 
        }
        return f;
    }
}