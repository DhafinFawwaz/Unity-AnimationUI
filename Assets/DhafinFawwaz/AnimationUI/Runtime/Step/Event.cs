using System;
using UnityEngine;
using UnityEngine.Events;

namespace DhafinFawwaz.AnimationUI {

    [Serializable, BGColor("#00FFFF15")]
    public class Event : Step, IExecutable
    {
        public UnityEvent UnityEvent;

        public void Execute(){
#if UNITY_EDITOR
            if(Application.isPlaying)
#endif
            UnityEvent?.Invoke();
            
            EditorOnlyProgress = 1;
        }

        public void Dexecute(){
            // nothing
            EditorOnlyProgress = 0;
        }

        public override string GetDisplayName(){
            return $"[UnityEvent]";
        }
    }
}
