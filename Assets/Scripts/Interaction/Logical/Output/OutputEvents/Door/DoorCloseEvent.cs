using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/Output Events/Door/Close")]
public class DoorCloseEvent : OutputEvent
{
    public override void Fire(GameObject _subject)
    {
        BaseDoor baseDoor = _subject.GetComponent<BaseDoor>();
        if (baseDoor != null) {
            baseDoor.Close();
        }
    }
}
