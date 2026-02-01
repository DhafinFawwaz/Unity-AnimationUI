using System;
using UnityEngine;
using UnityEngine.Events;

namespace DhafinFawwaz.AnimationUI {

    /// <summary>
    /// A step that invokes a UnityEvent when the animation reaches this point.
    /// Useful for triggering custom logic at specific points in the animation.
    /// </summary>
    [Serializable, BGColor("#00FFFF15")]
    public class Event : Step, IExecutable
    {
        /// <summary>
        /// The UnityEvent to invoke.
        /// </summary>
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
