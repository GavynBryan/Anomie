using UnityEngine;


public abstract class OutputEvent : ScriptableObject
{
    public abstract void Fire(GameObject _subject);
}
