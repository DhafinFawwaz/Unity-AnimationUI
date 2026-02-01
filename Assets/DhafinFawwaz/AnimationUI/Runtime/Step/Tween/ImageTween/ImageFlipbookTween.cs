using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class ImageFlipbookTween : ImageTween<Image, int, int, float>
    {
        public List<Sprite> Sprites;
        protected override Func<int, int, float, float> InterpolationFunction => (a, b, t) => Mathf.LerpUnclamped(a, b, t);
        public override void ApplyInterpolation(float value) => Target.sprite = Sprites[Mathf.RoundToInt(value)];
        public override void SetFromAsTargetValue() => From = Sprites.IndexOf(Target.sprite);
        public override void SetToAsTargetValue() => To = Sprites.IndexOf(Target.sprite);
    }
}
