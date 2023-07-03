using UnityEngine;


[CreateAssetMenu(menuName = "Output Events/Counter/Decrement")]
public class CounterDecrementEvent : OutputEvent
{
    public override void Fire(GameObject _subject)
    {
        CounterEntity counterEntity = _subject.GetComponent<CounterEntity>();
        if (counterEntity != null) {
            counterEntity.Decrement();
        }
    }
}
