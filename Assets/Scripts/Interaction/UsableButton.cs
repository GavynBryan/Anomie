
using UnityEngine;
using UnityEngine.Events;

public class UsableButton : MonoBehaviour, IInteractable
{
    [SerializeField] public OutputCommand[] onPressedOutput;

    [SerializeField] private bool useOnce;
    [SerializeField] private float cooldown;

    [SerializeField]private AudioSource useSound;
    private bool hasBeenUsed;
    private float lastPressed = Mathf.NegativeInfinity;

    public void OnInteract(PlayerController player)
    {
        if(Time.time - lastPressed < cooldown || (useOnce && hasBeenUsed)) {
            return;
        }
        OnPressed();
    }

    private void OnPressed()
    { 
        foreach(var o in onPressedOutput) {
            o.Fire();
        }
        if (useSound != null) {
            useSound.Play();
        }
        lastPressed = Time.time;
        hasBeenUsed = true;
    }
}