using System.Collections;
using UnityEngine;

public enum Doorstate
{
    CLOSED = 0,
    OPEN,
    CLOSING,
    OPENING
}

[RequireComponent(typeof(BoxCollider))]
public abstract class BaseDoor : MonoBehaviour, IInteractable
{
    protected BoxCollider boxCollider;

    protected Doorstate currentState;
    [SerializeField]protected bool usable;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void OnInteract(PlayerController _player)
    {
        if(usable && (currentState == Doorstate.CLOSED || currentState == Doorstate.CLOSING)) {
            Open();
        } else {
            Close();
        }
    }

    public abstract void Open();
    public abstract void Close();
}