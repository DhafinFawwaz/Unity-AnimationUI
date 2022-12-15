using UnityEngine;
using UnityEngine.Audio;

#if UNITY_EDITOR
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
#endif
}
