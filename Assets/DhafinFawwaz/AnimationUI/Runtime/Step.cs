using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {

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

        public virtual string GetDisplayName() => "";
    }
}
