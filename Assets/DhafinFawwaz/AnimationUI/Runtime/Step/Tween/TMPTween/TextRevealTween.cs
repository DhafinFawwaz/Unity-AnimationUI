using System;
using UnityEngine;
using TMPro;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class TextRevealTween : TMPTween<TMP_Text, int, int, float>
    {
        protected override Func<int, int, float, float> InterpolationFunction => (a, b, t) => Mathf.LerpUnclamped(a, b, t);
        public override void ApplyInterpolation(float value) => Target.maxVisibleCharacters = Mathf.RoundToInt(value);
        public override void SetFromAsTargetValue() => From = 0;
        public override void SetToAsTargetValue() => To = Target.text.Length;
#if UNITY_EDITOR
        public TextRevealTween(){
            UnityEditor.EditorApplication.delayCall += () => {
                From = 0;
                if(Target.text != null) To = Target.text.Length;
            };
        }
#endif
    }
}
