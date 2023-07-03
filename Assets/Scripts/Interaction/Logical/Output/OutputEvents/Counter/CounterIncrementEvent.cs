using UnityEngine;


[CreateAssetMenu(menuName = "Output Events/Counter/Increment")]
public class CounterIncrementEvent : OutputEvent
{
    public override void Fire(GameObject _subject)
    {
        CounterEntity counterEntity = _subject.GetComponent<CounterEntity>();
        if (counterEntity != null) {
            counterEntity.Increment();
        }
    }
}
