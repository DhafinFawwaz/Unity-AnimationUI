using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _soundSource;
    // [SerializeField] AudioMixer _soundMixer;
    [SerializeField] AudioClip _defaultSound;

    public void PlaySound(AudioClip audioClip, float volume)
    {
        if(audioClip == null)
        {
            PlayDefaultSound();
            return;
        }
        _soundSource.PlayOneShot(audioClip, volume);
    }
    public void PlaySound(AudioClip audioClip)
    {
        if(audioClip == null)
        {
            PlayDefaultSound();
            return;
        }
        _soundSource.PlayOneShot(audioClip);
    }
    void PlayDefaultSound()
    {
        _soundSource.PlayOneShot(_defaultSound);
    }

    [System.Serializable] public class Sound
    {
        [Tooltip("Clip to play")]public AudioClip Clip;
        [Tooltip("Volume of the clip")]
        public float Volume = 1;
        #if UNITY_EDITOR 
        [Tooltip("Just for naming, this isn't actually used anywhere")]public string ClipName;
        #endif
    }
    public Sound[] SFX;
    public void PlaySound(int index)
    {
        PlaySound(SFX[index].Clip, SFX[index].Volume);
    }
}
