using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable, BGColor("#e600ff15")]
    public class SetTargetAsFrom : Step, IExecutable, IInjectable
    {
        [SerializeField] AnimationUI _animationUI;
        [SerializeField] int _index = 0;


        public (bool, string) GetDisplayStep() {
            if(_animationUI == null) return (false, "AnimationUI is null");
            if(_index < 0) _index = 0;
            if(_index >= _animationUI.Sequence.Count) _index = _animationUI.Sequence.Count - 1;

            var step = _animationUI.Get<Step>(_index);
            bool isTweenable = step is ITweenable;
            var className = step.GetType().Name;
            return (isTweenable, $"[{className}] {step.GetDisplayName()}");
        }

        public void Inject(object injected)
        {
            if(_animationUI == null && injected is AnimationUI aui){
                _animationUI = aui;
            }
        }

        public void Execute()
        {
#if UNITY_EDITOR
            if(Application.isPlaying)
#endif
            SetTargetAsFromExecute();


            EditorOnlyProgress = 1;
        }

        void SetTargetAsFromExecute()
        {
            if(_animationUI == null) return;
            if(_index < 0) _index = 0;
            if(_index >= _animationUI.Sequence.Count) _index = _animationUI.Sequence.Count - 1;

            var step = _animationUI.Get<Step>(_index);
            if(step is ITweenable tweenableStep) tweenableStep.SetFromAsTargetValueSafe();
        }

        public void Dexecute()
        {
            EditorOnlyProgress = 0;
        }
        
        public override string GetDisplayName(){
            return "Set Target As From";
        }
    }
}
