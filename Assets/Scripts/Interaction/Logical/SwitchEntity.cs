using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class SwitchEntity : MonoBehaviour
{
    [SerializeField] public EntityOutput onTrue;
    [SerializeField] public EntityOutput onFalse;

    bool state = false;

    public void Toggle()
    {
        if(state) {
            state = false;
            onFalse.Invoke();
        } else {
            state = true;
            onTrue.Invoke();
        }
    }
}
