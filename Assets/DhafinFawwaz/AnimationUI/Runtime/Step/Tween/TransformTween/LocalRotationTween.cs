using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class LocalRotationTween : TransformTween<Transform, Vector3, Vector3, Quaternion>
    {
        protected override Func<Vector3, Vector3, float, Quaternion> InterpolationFunction => (a, b, t) => Quaternion.SlerpUnclamped(Quaternion.Euler(a), Quaternion.Euler(b), t);
        public override void ApplyInterpolation(Quaternion value) => Target.localRotation = value;
        public override void SetFromAsTargetValue() => From = Target.localEulerAngles;
        public override void SetToAsTargetValue() => To = Target.localEulerAngles;
    }
}
