using UnityEngine;
using AnimationUISingleton;
// Please modify this class
public class Customizable
{
    public static void SetActiveAllInput(bool isActivating)
    {
        // Please modify this line to use your own Singleton class.
        Debug.Log("Set Active All Input");
        Singleton.Instance.Game.SetActiveAllInput(isActivating);
    }
    public static void PlaySound(AudioClip _SFXFile)
    {
        // Please modify this line to use your own Singleton class.
        Debug.Log("SFX by file");
        Singleton.Instance.Audio.PlaySound(_SFXFile);
    }
    public static void PlaySound(int _index)
    {
        // Please modify this line to use your own Singleton class.
        Debug.Log("SFX by index");
        Singleton.Instance.Audio.PlaySound(_index);
    }
}
