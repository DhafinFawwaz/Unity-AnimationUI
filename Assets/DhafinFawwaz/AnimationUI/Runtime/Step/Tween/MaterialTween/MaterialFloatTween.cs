using System;
using UnityEngine;
using UnityEngine.UI;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class MaterialFloatTween : MaterialTween<Material, float, float, float>
    {
        public string MaterialProperty;
        protected override Func<float, float, float, float> InterpolationFunction => Mathf.LerpUnclamped;
        public override void ApplyInterpolation(float value) => Target.SetFloat(MaterialProperty, value);
        public override void SetFromAsTargetValue() => From = Target.GetFloat(MaterialProperty);
        public override void SetToAsTargetValue() => To = Target.GetFloat(MaterialProperty);
    }
}
