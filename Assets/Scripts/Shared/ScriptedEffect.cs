using UnityEngine;

public abstract class ScriptedEffect<T, U> : ScriptableObject
{
    public virtual void Init(T controller) { }
    public abstract U Trigger(T controller);

    public virtual void UpdateEffect(T controller) { }
}
