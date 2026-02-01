using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable, BGColor("#FFFF0015")]
    public class SFX : Step, IExecutable
    {
        public AudioSource Source;
        public AudioClip Clip;

#if UNITY_EDITOR
        SFX(){
            UnityEditor.EditorApplication.delayCall += () => {
                var focusedObject = UnityEditor.Selection.activeGameObject;
                if(focusedObject != null) Source = focusedObject.GetComponent<AudioSource>();
                if(Source == null) Source = GameObject.FindAnyObjectByType<AudioSource>();
                if(Source == null) Debug.LogWarning("There seems to be no AudioSource in the scene. You may want to add one. Adding this Step can automatically assign the AudioSource to the Step.");
            };
        }
#endif

        public void Execute(){
#if UNITY_EDITOR
            if(Application.isPlaying)
#endif
            if(Clip != null) Source.PlayOneShot(Clip);
            EditorOnlyProgress = 1;
        }

        public void Dexecute(){
            // nothing
            EditorOnlyProgress = 0;
        }

        public override string GetDisplayName(){
            if(Clip == null) return "[null]";
            return $"[{Clip.name}] [{Clip.length} s]";
        }
    }
}
