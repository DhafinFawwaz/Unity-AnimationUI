using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class AlphaTween : CanvasGroupTween<CanvasGroup, float, float, float>
    {
        protected override Func<float, float, float, float> InterpolationFunction => Mathf.LerpUnclamped;
        public override void ApplyInterpolation(float value) => Target.alpha = value;
        public override void SetFromAsTargetValue() => From = Target.alpha;
        public override void SetToAsTargetValue() => To = Target.alpha;
    }
}
