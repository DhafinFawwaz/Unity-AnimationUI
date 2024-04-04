using UnityEngine;

namespace DhafinFawwaz.AnimationUILib.Demo
{
public class Main
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialization()
    {
        Singleton.Initialize();
    }
}

}