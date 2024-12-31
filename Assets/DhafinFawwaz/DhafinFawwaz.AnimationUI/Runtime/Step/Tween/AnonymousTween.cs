using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    // The only reason this exist is because C# doesn't support anonymous class that implements interface
    public class AnonymousTween<TComponent, UFrom, VTo, WInterpolationOutput> : Tween<TComponent, UFrom, VTo, WInterpolationOutput> where TComponent : Component
    {
        Action<WInterpolationOutput> _applyInterpolation;
        public AnonymousTween(TComponent component, Func<UFrom, VTo, float, WInterpolationOutput> InterpolationFunction, Action<WInterpolationOutput> ApplyInterpolation) {
            Target = component;
            _lerpFunction = InterpolationFunction;
            _applyInterpolation = ApplyInterpolation;
        }

        public AnonymousTween(
            TComponent component, Func<UFrom, VTo, float, WInterpolationOutput> InterpolationFunction, Action<WInterpolationOutput> ApplyInterpolation,
            UFrom from, VTo to, float duration, Ease easeType
        ) {
            Target = component;
            _lerpFunction = InterpolationFunction;
            _applyInterpolation = ApplyInterpolation;
            From = from;
            To = to;
            Duration = duration;
            EaseType = easeType;
        }

        protected override Func<UFrom, VTo, float, WInterpolationOutput> InterpolationFunction => _lerpFunction;
        public override void ApplyInterpolation(WInterpolationOutput value) => _applyInterpolation.Invoke(value);
    }
}
