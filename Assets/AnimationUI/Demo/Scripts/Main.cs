using UnityEngine;

public class Main
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialization()
    {
        Singleton.Initialize();
    }
}
