using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private ObjectPoolManager  objectPool;
    public static GameManager Instance
    {
        get { return instance; }
    }
    public static ObjectPoolManager ObjectPool
    {
        get { return instance.objectPool; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        objectPool = new ObjectPoolManager();

        DontDestroyOnLoad(gameObject);
    }
}