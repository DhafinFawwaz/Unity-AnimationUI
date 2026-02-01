using System;
using UnityEngine;
using UnityEngine.Events;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class RotationTween : TransformTween<Transform, Vector3, Vector3, Quaternion>
    {
        [SerializeField] UnityEvent OnBruh;
        protected override Func<Vector3, Vector3, float, Quaternion> InterpolationFunction => (a, b, t) => Quaternion.SlerpUnclamped(Quaternion.Euler(a), Quaternion.Euler(b), t);
        public override void ApplyInterpolation(Quaternion value) => Target.rotation = value;
        public override void SetFromAsTargetValue() => From = Target.eulerAngles;
        public override void SetToAsTargetValue() => To = Target.eulerAngles;
    }
}
