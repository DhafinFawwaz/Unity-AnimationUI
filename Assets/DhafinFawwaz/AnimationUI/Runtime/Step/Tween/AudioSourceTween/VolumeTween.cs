using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class VolumeTween : AudioSourceTween<AudioSource, float, float, float>
    {
        protected override Func<float, float, float, float> InterpolationFunction => Mathf.LerpUnclamped;
        public override void ApplyInterpolation(float value) => Target.volume = value;
        public override void SetFromAsTargetValue() => From = Target.volume;
        public override void SetToAsTargetValue() => To = Target.volume;
    }
}
