using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class PositionTween : TransformTween<Transform, Vector3, Vector3, Vector3>
    {
        protected override Func<Vector3, Vector3, float, Vector3> InterpolationFunction => Vector3.LerpUnclamped;
        public override void ApplyInterpolation(Vector3 value) => Target.position = value;
        public override void SetFromAsTargetValue() => From = Target.position;
        public override void SetToAsTargetValue() => To = Target.position;
    }
}
