using UnityEngine;

namespace DhafinFawwaz.AnimationUILib.Demo
{

#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]
#endif
public class Singleton : MonoBehaviour
{
    public AudioManager Audio;
    public GameManager Game;
    public static Singleton Instance => _instance;
    static Singleton _instance;

    /// <summary>
    /// Nulls any existing instance and creates a new one. It makes sure no weird
    /// things happen when you reload the scene in the editor. It's used in Main.cs
    /// </summary>
    public static void Initialize()
    {
        if(_instance != null)
        {
            Destroy(_instance.gameObject);
            _instance = null;
        }
        GameObject.Instantiate(Resources.Load("SINGLETON"));
    }
    
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
}

}