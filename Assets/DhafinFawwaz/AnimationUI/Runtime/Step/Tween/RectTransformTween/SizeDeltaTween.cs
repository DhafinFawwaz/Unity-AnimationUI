using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class SizeDeltaTween : RectTransformTween<RectTransform, Vector2, Vector2, Vector2>
    {
        protected override Func<Vector2, Vector2, float, Vector2> InterpolationFunction => Vector2.LerpUnclamped;
        public override void ApplyInterpolation(Vector2 value) => Target.sizeDelta = value;
        public override void SetFromAsTargetValue() => From = Target.sizeDelta;
        public override void SetToAsTargetValue() => To = Target.sizeDelta;
    }
}
