using UnityEngine;


[CreateAssetMenu(menuName = "Output Events/Door/Open")]
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
