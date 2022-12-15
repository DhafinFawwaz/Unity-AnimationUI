using UnityEngine;
using UnityEngine.Audio;

public class Singleton : MonoBehaviour
{
    public AudioManager Audio;
    public GameManager Game;
    public static Singleton Instance;
    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
