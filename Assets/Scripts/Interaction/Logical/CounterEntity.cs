using System;
using UnityEngine;

[Serializable]
public class CounterOutput
{
    public int Index;
    public OutputCommand Event;
}

public class CounterEntity : MonoBehaviour
{
    [SerializeField] EntityOutput onMaxValue;
    [SerializeField] EntityOutput onIncrement;

    [SerializeField] private int maxValue;
    [SerializeField] private bool loop;

    private int currentValue = 0;

    public void Restart()
    {
        currentValue = 0;
    }

    public void Increment()
    {
        currentValue++;
        onIncrement.Invoke();
        if(currentValue >= maxValue) {
            onMaxValue.Invoke();
            if(loop) { 
                Restart();
            }else {
                gameObject.SetActive(false);
            }
        }
    }

    public void Decrement()
    {
        currentValue--;
        if(currentValue < 0) { currentValue = 0; }
        onIncrement.Invoke();
    }

}
