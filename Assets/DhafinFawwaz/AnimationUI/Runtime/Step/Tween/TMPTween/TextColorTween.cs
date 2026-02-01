using System;
using UnityEngine;
using TMPro;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class TextColorTween : TMPTween<TMP_Text, Color, Color, Color>
    {
        protected override Func<Color, Color, float, Color> InterpolationFunction => Color.LerpUnclamped;
        public override void ApplyInterpolation(Color value) => Target.color = value;
        public override void SetFromAsTargetValue() => From = Target.color;
        public override void SetToAsTargetValue() => To = Target.color;
    }
}
