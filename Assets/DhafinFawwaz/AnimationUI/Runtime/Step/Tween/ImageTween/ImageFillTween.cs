using System;
using UnityEngine;
using UnityEngine.UI;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class ImageFillTween : ImageTween<Image, float, float, float>
    {
        protected override Func<float, float, float, float> InterpolationFunction => Mathf.LerpUnclamped;
        public override void ApplyInterpolation(float value) => Target.fillAmount = value;
        public override void SetFromAsTargetValue() => From = Target.fillAmount;
        public override void SetToAsTargetValue() => To = Target.fillAmount;
    }
}
