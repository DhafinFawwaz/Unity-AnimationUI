using System;
using UnityEngine;
using TMPro;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class CountdownTween : TMPTween<TMP_Text, int, int, float>
    {
        protected override Func<int, int, float, float> InterpolationFunction => (a, b, t) => Mathf.LerpUnclamped(a, b, t);
        public override void ApplyInterpolation(float value) => Target.text = Mathf.RoundToInt(value).ToString();
        public override void SetFromAsTargetValue() => int.TryParse(Target.text, out From);
        public override void SetToAsTargetValue() => int.TryParse(Target.text, out To);
    }
}
