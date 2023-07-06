using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/Output Events/Switch/Toggle")]
public class SwitchToggleEvent : OutputEvent
{
    public override void Fire(GameObject _subject)
    {
        SwitchEntity switchEntity = _subject.GetComponent<SwitchEntity>();
        if (switchEntity != null) {
            switchEntity.Toggle();
        }
    }
}
