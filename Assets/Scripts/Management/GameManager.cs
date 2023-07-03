using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static object lockObject = new object();

    public static GameManager Instance
    {
        get
        {
            if (instance == null) {
                new GameObject();
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
}