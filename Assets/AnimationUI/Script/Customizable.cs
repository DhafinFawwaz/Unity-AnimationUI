using UnityEngine;

namespace DhafinFawwaz.AnimationUILib
{

// Please modify this class
public static class AnimationUICustomizable
{
    public static void SetActiveAllInput(bool isActivating)
    {
        Debug.Log("Set Active All Input");
        // Please modify this line to use your own Singleton class.
        // Singleton.Instance.Game.SetActiveAllInput(isActivating);
    }
    public static void PlaySound(AudioClip _SFXFile)
    {
        Debug.Log("SFX by file");
        // Please modify this line to use your own Singleton class.
        // Singleton.Instance.Audio.PlaySound(_SFXFile);
    }
    public static void PlaySound(int _index)
    {
        Debug.Log("SFX by index");
        // Please modify this line to use your own Singleton class.
        // Singleton.Instance.Audio.PlaySound(_index);
    }
}
}
