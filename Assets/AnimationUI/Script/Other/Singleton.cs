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
    public static Singleton Instance;
    
    void Awake()
    {
        if(Instance == null)Instance = this;

#if UNITY_EDITOR
        else if(!Application.isPlaying)DestroyImmediate(gameObject);
#endif
        else Destroy(gameObject);

#if UNITY_EDITOR
        if(Application.isPlaying)
#endif
        DontDestroyOnLoad(gameObject);

    }
    

#if UNITY_EDITOR
    // So that access to Singleton exist in edit mode
    void OnEnable()
    {
        if(Application.isPlaying)return;

        if(Instance == null)Instance = this;
        // else DestroyImmediate(gameObject);
    }
    public static void LoadSingleton()
    {
        if(Singleton.Instance == null)
        {
            GameObject singleton = Resources.Load("SINGLETON") as GameObject;
            if(singleton == null)
            {
                Debug.Log("SINGLETON prefab not found in .../Resources/SINGLETON. Please don't remove or move this to other folder.", singleton);
                return;
            }

            PrefabUtility.InstantiatePrefab(singleton);
            if(Singleton.Instance == null)
            {
                Debug.Log("Something went wrong with ", Singleton.Instance);
                return;
            }
        }
    }
#endif
}
