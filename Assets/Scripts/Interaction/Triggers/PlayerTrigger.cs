using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : Trigger
{
    [SerializeField] private bool once;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null) {
            onTrigger.Invoke();
            if (once) {
                gameObject.SetActive(false);
            }
        }
    }
}