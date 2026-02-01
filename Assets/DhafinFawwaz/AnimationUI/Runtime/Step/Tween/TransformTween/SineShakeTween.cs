using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class SineShakeTween : TransformTween<Transform, Vector3, Vector3, Vector3>
    {
        protected override Func<Vector3, Vector3, float, Vector3> InterpolationFunction => (from, to, t) => Vector3.LerpUnclamped(from, to, shake(t));
        public override void ApplyInterpolation(Vector3 value) => Target.position = value;
        public override void SetFromAsTargetValue() => From = Target.position;
        public override void SetToAsTargetValue() => To = Target.position;

        [SerializeField] float _frequency = 2f;
        float g(float x) => Mathf.Sin(x * Mathf.PI * 2f * _frequency);
        // float f(float x) => - (2*x-1) * (2*x-1) + 1;
        float f(float x) => -x+1;
        float shake(float x) => g(x) * f(x);
    }
}
