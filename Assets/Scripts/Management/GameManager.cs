using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Object> resources;

    private static GameManager instance;
    private EffectsManager effects;
    
    public static GameManager Instance
    {
        get { return instance; }
    }

    public static EffectsManager Effects
    { get { return instance.effects; } }


    private GameManager()
    {
        resources = new List<Object>();
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;

        var library = Resources.Load("Data/EffectsLibrary") as EffectsLibrary;
        resources.Add(library);
        effects = new EffectsManager(library);

        DontDestroyOnLoad(gameObject);
    }
}