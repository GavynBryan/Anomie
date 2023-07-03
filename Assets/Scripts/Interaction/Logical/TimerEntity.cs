using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class TimerEntity : MonoBehaviour 
{
    [SerializeField] public EntityOutput onTimerFinished;

    [SerializeField] private float maxTime;
    [SerializeField] private bool loop;

    private float timeSinceStarted = Mathf.NegativeInfinity;

    public void Restart()
    {
        timeSinceStarted = Time.time;
    }

    void OnEnable()
    {
        Restart();
    }

    void Update()
    {
        if (Time.time - timeSinceStarted >= maxTime) {
            onTimerFinished.Invoke();
            if (loop) {
                Restart();
            } else {
                gameObject.SetActive(false);
            }
        }
    }
}
