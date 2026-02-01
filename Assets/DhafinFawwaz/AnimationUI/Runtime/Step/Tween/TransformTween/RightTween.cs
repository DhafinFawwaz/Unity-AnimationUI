using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class RightTween : TransformTween<Transform, Vector3, Vector3, Vector3>
    {
        protected override Func<Vector3, Vector3, float, Vector3> InterpolationFunction => Vector3.LerpUnclamped;
        public override void ApplyInterpolation(Vector3 value) => Target.right = value;
        public override void SetFromAsTargetValue() => From = Target.right;
        public override void SetToAsTargetValue() => To = Target.right;
    }
}
