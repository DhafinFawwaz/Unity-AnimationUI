using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class ThrownLocalTween : TransformTween<Transform, Vector3, Vector3, Vector3>
    {
        public float Height = 5;
        public float Power = 2;
        float Parabole(float x, float p) {
            if(x <= 0.5f) return -Mathf.Pow(1-2*x, p) + 1;
            return -Mathf.Pow(2*x-1, p) + 1;
        }
        protected override Func<Vector3, Vector3, float, Vector3> InterpolationFunction => (a, b, t) => {
            Vector3 res = Vector3.LerpUnclamped(a, b, t);
            res.y += Parabole(t, Power) * Height;
            return res;
        };
        public override void ApplyInterpolation(Vector3 value) => Target.localPosition = value;
        public override void SetFromAsTargetValue() => From = Target.localPosition;
        public override void SetToAsTargetValue() => To = Target.localPosition;
    }
}
