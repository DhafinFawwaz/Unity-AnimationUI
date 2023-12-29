namespace AnimationUISingleton
{
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class Singleton : MonoBehaviour
{
    public AudioManager Audio;
    public GameManager Game;
    public static Singleton Instance
    {
        get
        {
            if(_instance == null)LoadSingleton();
            return _instance;
        }
    }
    static Singleton _instance;

    
    void Awake()
    {
        if(_instance == null)_instance = this;

#if UNITY_EDITOR
        else if(!Application.isPlaying)DestroyImmediate(gameObject);
#endif
        else Destroy(gameObject);

#if UNITY_EDITOR
        if(Application.isPlaying)
#endif
        DontDestroyOnLoad(gameObject);

    }
    
    public static void LoadSingleton()
    {
        Singleton singleton = FindObjectOfType<Singleton>();
        if(singleton != null)
        {
            Debug.Log("Found singleton in scene");
            _instance = singleton;
            return;
        }
        singleton = (Resources.Load("SINGLETON") as GameObject).GetComponent<Singleton>();
        if(singleton == null)
        {
            Debug.Log("SINGLETON prefab not found in .../Resources/SINGLETON. Please don't remove or move this to other folder.", singleton);
            return;
        }

#if UNITY_EDITOR
        UnityEditor.PrefabUtility.InstantiatePrefab(singleton);
#else
        Instantiate(singleton);
#endif

        if(_instance == null)
        {
            Debug.Log("Something went wrong with loading singleton", Singleton._instance);
            return;
        }
        Debug.Log("Automatically loaded Singleton from .../Resources/SINGLETON");
    }
}

}