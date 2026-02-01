using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class LocalEulerAnglesTween : TransformTween<Transform, Vector3, Vector3, Vector3>
    {
        protected override Func<Vector3, Vector3, float, Vector3> InterpolationFunction => Vector3.SlerpUnclamped;
        public override void ApplyInterpolation(Vector3 value) => Target.localEulerAngles = value;
        public override void SetFromAsTargetValue() => From = Target.localEulerAngles;
        public override void SetToAsTargetValue() => To = Target.localEulerAngles;
    }
}
