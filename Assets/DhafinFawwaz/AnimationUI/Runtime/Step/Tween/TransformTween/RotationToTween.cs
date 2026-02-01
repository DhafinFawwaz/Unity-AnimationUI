using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    // TransformTween<Component, From Vector Input, To Transform.position, InterpolationFunction will output Quaternion>
    public class RotationToTween : TransformTween<Transform, Vector3, float, Quaternion>
    {
        protected override Func<Vector3, float, float, Quaternion> InterpolationFunction 
            => (from, to, t) => Quaternion.SlerpUnclamped(Quaternion.Euler(from), default, t);

        public override void ApplyInterpolation(Quaternion value) => Target.rotation = value;
        public override void SetFromAsTargetValue() => From = Target.eulerAngles;
        public override void SetToAsTargetValue() => To = 100;
    }
}
