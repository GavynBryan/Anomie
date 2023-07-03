using UnityEngine;


[CreateAssetMenu(menuName = "Output Events/Switch/Toggle")]
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
