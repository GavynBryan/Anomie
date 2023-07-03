using System;
using UnityEngine;

[Serializable]
public class OutputCommand
{
    [SerializeField] private GameObject subject;
    [SerializeField] private OutputEvent Event;

    public void Fire()
    {
        Event.Fire(subject);
    }
}

[Serializable]
public class EntityOutput
{
    [SerializeField] private OutputCommand[] outputCommands;

    public void Invoke()
    {
        foreach (OutputCommand o in outputCommands) { 
            o.Fire();
        }
    }
}