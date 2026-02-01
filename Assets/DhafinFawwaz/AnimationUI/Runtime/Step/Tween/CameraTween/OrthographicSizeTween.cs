using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {


    [Serializable]
    public class OrthographicSizeTween : CameraTween<Camera, float, float, float>
    {
        protected override Func<float, float, float, float> InterpolationFunction => Mathf.LerpUnclamped;
        public override void ApplyInterpolation(float value) => Target.orthographicSize = value;
        public override void SetFromAsTargetValue() => From = Target.orthographicSize;
        public override void SetToAsTargetValue() => To = Target.orthographicSize;
    }
}
