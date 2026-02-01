using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class PositionToTween : TransformTween<Transform, Vector3, Transform, Vector3>
    {
        protected override Func<Vector3, Transform, float, Vector3> InterpolationFunction => (from, to, t) => Vector3.LerpUnclamped(from, to.position, t);
        public override void ApplyInterpolation(Vector3 value) => Target.position = value;
        public override void SetFromAsTargetValue() => From = Target.position;
        public override void SetToAsTargetValue() => To.position = Target.position;
    }
}
