using System.Collections;
using UnityEngine;

public class SlidingDoor : BaseDoor
{
    private Vector3 openPosition; 
    private Vector3 closePosition;
    private Vector3 targetPosition;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip moveSound;
    [SerializeField] private AudioClip shutSound;

    [SerializeField] private float speed;
    [SerializeField] private float slack;


    void Start()
    {
        closePosition = transform.position;
        openPosition = transform.position + new Vector3(0, boxCollider.size.y + slack, 0);
    }
    public override void Open()
    {
        if (currentState != Doorstate.OPEN) {
            currentState = Doorstate.OPENING;
            audioSource.PlayOneShot(moveSound);
            targetPosition = openPosition;
        }
    }
    public override void Close()
    {
        if (currentState != Doorstate.CLOSED) {
            currentState = Doorstate.CLOSING;
            audioSource.PlayOneShot(moveSound);
            targetPosition = closePosition;
        }
    }

    void Update()
    {
        if (currentState == Doorstate.OPENING || currentState == Doorstate.CLOSING) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, targetPosition) <= 0.1) {
                audioSource.PlayOneShot(shutSound);
                transform.position = targetPosition;
                //Works due to the way the enums are ordered
                currentState -= 2;
            }
        }    
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = collision.GetContact(0).normal;
        if(direction == transform.rotation * Vector3.down) {
            Debug.Log("Hit from the bottom of door");
        }
        Debug.Log("Entered something");
    }
}