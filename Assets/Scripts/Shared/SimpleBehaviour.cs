using UnityEngine;

/// <summary>
/// Behavior class for weapons or other objects
/// </summary>
public class SimpleBehaviour<T> : ScriptableObject
{
    public virtual void Execute(T controller) { }
}