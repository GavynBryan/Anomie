using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/Output Events/Generic/Toggle Event")]
public class GenericToggleEvent : OutputEvent
{
    public override void Fire(GameObject _subject)
    {
        _subject.SetActive(!_subject.activeInHierarchy);
    }
}
