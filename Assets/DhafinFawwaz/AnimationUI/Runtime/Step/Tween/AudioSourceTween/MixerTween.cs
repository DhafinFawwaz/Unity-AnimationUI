using System;
using UnityEngine;
using UnityEngine.Audio;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class MixerTween : AudioSourceTween<AudioMixer, float, float, float>
    {
        public string ParameterKey;
        protected override Func<float, float, float, float> InterpolationFunction => Mathf.LerpUnclamped;
        public override void ApplyInterpolation(float value) => Target.SetFloat(ParameterKey, value);
        public override void SetFromAsTargetValue() => Target.GetFloat(ParameterKey, out From);
        public override void SetToAsTargetValue() => Target.GetFloat(ParameterKey, out From);
    }
}
