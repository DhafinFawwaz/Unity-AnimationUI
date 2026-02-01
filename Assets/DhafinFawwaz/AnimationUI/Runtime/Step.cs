using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {

    /// <summary>
    /// Base class for all animation steps (tweens, waits, executables, etc.).
    /// Inherit from this to create custom step types.
    /// </summary>
    [Serializable, AutoTypeMenu]
    public abstract class Step
    {
#if UNITY_EDITOR

        [SerializeField, HideInInspector]
        string _name = "";
#endif

        [HideInInspector]
        public string EditorOnlyName {
#if UNITY_EDITOR
            get => _name; 
            set => _name = value;
#else
            get => ""; 
            set {}
#endif
        }

#if UNITY_EDITOR
        [HideInInspector]
        float _progress = 0f;
#endif
        public float EditorOnlyProgress {
#if UNITY_EDITOR
            get => _progress; 
            set => _progress = value;
#else
            get => 0; 
            set {}
#endif
        }

        /// <summary>
        /// Override this to provide a custom display name for the step in the inspector.
        /// Default returns empty string.
        /// </summary>
        /// <returns>The display name shown in the inspector</returns>
        public virtual string GetDisplayName() => "";
    }
}
