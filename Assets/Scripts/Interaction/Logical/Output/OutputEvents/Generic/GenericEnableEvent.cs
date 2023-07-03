using UnityEngine;


[CreateAssetMenu(menuName = "Output Events/Generic/Enable Event")]
public class GenericEnableEvent : OutputEvent
{
    public override void Fire(GameObject _subject)
    {
        _subject.SetActive(true);
    }
}
