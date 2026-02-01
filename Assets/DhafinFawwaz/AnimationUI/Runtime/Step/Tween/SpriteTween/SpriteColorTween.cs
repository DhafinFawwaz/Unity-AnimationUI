using System;
using UnityEngine;
using UnityEngine.UI;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class SpriteColorTween : SpriteTween<SpriteRenderer, Color, Color, Color>
    {
        protected override Func<Color, Color, float, Color> InterpolationFunction => Color.LerpUnclamped;
        public override void ApplyInterpolation(Color value) => Target.color = value;
        public override void SetFromAsTargetValue() => From = Target.color;
        public override void SetToAsTargetValue() => To = Target.color;
    }
}
