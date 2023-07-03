using UnityEngine;


[CreateAssetMenu(menuName = "Output Events/Generic/Disable Event")]
public class GenericDisableEvent : OutputEvent
{
    public override void Fire(GameObject _subject)
    {
        _subject.SetActive(false);
    }
}
