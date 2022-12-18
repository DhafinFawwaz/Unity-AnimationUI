using UnityEngine;
using UnityEngine.Audio;

#if UNITY_EDITOR
using UnityEditor;
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
        GameObject singleton = Resources.Load("SINGLETON") as GameObject;
        if(singleton == null)
        {
            Debug.Log("SINGLETON prefab not found in .../Resources/SINGLETON. Please don't remove or move this to other folder.", singleton);
            return;
        }

        PrefabUtility.InstantiatePrefab(singleton);
        if(_instance == null)
        {
            Debug.Log("Something went wrong with loading singleton", Singleton._instance);
            return;
        }
        Debug.Log("Automatically loaded Singleton from .../Resources/SINGLETON");
    }
}
