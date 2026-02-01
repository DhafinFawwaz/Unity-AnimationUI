using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable, BGColor("#00ff0015")]
    public class SetActive : Step, IExecutable, IReverseSequenceHandler
    {
        [SerializeField] GameObject Target;
        public bool ToActive = true;
        public void Execute()
        {
            if(Target != null && Target.activeSelf != ToActive) {
                Target.SetActive(ToActive);
                EditorOnlyProgress = 1;
            }
        }

        public void Dexecute()
        {
            if(Target != null && Target.activeSelf != !ToActive) {
                Target.SetActive(!ToActive);
                EditorOnlyProgress = 0;
            }
        }

        public void OnSequenceReversed()
        {
            ToActive = !ToActive;
        }
        
        public override string GetDisplayName(){
            if(Target != null && ToActive) return $"[{Target.gameObject.name}] [Activate]";
            if(Target != null && !ToActive) return $"[{Target.gameObject.name}] [Deactivate]";
            if(ToActive) return "[null] [Activate]";
            return "[null] [Deactivate]";
        }
    }
}
