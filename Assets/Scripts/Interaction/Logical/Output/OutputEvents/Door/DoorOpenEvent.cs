using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/Output Events/Door/Open")]
public class DoorOpenEvent : OutputEvent
{
    public override void Fire(GameObject _subject)
    {
        BaseDoor baseDoor = _subject.GetComponent<BaseDoor>();
        if (baseDoor != null) {
            baseDoor.Open();
        }
    }
}
