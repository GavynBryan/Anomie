using UnityEngine;

/// <summary>
/// Behavior class for weapons or other objects
/// </summary>
public class WeaponBehavior<T> : ScriptableObject
{
    public virtual void Execute(T controller, int shots) { }
}