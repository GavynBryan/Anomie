using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Trigger : MonoBehaviour
{
    [SerializeField] protected EntityOutput onTrigger;

    void Awake()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        var collider = gameObject.GetComponent<BoxCollider>();
        if (renderer != null) {
            renderer.enabled = false;
        }
        if(collider != null ) {
            collider.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        onTrigger.Invoke();
    }
}