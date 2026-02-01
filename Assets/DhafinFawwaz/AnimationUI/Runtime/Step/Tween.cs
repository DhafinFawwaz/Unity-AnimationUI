using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {

    /// <summary>
    /// Base class for all tweens that interpolate a value from From to To over time.
    /// </summary>
    /// <typeparam name="TComponent">The Unity component being animated (Transform, Image, etc.)</typeparam>
    /// <typeparam name="UFrom">Type of the FROM value</typeparam>
    /// <typeparam name="VTo">Type of the TO value (usually same as UFrom)</typeparam>
    /// <typeparam name="WInterpolationOutput">Return type of the interpolation function (usually same as UFrom)</typeparam>
    [BGColor("#ff000015")]
    public abstract class Tween<TComponent, UFrom, VTo, WInterpolationOutput> : Step, ITweenable, IReverseSequenceHandler where TComponent : UnityEngine.Object
    {
        /// <summary>
        /// The component being animated.
        /// </summary>
        public TComponent Target;
        public void SetTarget(UnityEngine.Object target) {
            Target = target as TComponent;
            OnTargetAssigned();
        }
        public UnityEngine.Object GetTarget() => Target;
        
        /// <summary>
        /// Duration of the tween in seconds.
        /// </summary>
        public float Duration = 0.5f;
        public float GetDuration() => Duration;
        
        /// <summary>
        /// The easing function to use for interpolation.
        /// </summary>
        public Ease EaseType = Ease.OutQuart;
        
        /// <summary>
        /// The starting value for the animation.
        /// </summary>
        public UFrom From;
        
        /// <summary>
        /// The ending value for the animation.
        /// </summary>
        public VTo To;
        
        /// <summary>
        /// Applies the interpolated value to the target component.
        /// Override this to define where/how the value is applied (e.g., Target.position = value).
        /// </summary>
        /// <param name="value">The interpolated value to apply</param>
        public abstract void ApplyInterpolation(WInterpolationOutput value);
        public void ApplyInterpolationSafe(WInterpolationOutput value) {
            if(Target == null) return;
            ApplyInterpolation(value);
        }
        
        public void UpdateToFrom() {
            ApplyInterpolationSafe(_lerpFunction(From, To, 0));
#if UNITY_EDITOR
            EditorOnlyProgress = 0;
#endif
        }   
        public void UpdateToTo() {
            ApplyInterpolationSafe(_lerpFunction(From, To, 1));
#if UNITY_EDITOR
            EditorOnlyProgress = 1;
#endif
        }        
        public void Update(float time) => UpdateNormalized(time/Duration);
        public virtual void UpdateNormalized(float t) {
            ApplyInterpolationSafe(_lerpFunction(From, To, EaseFunction.Evaluate(t, EaseType)));
#if UNITY_EDITOR
            EditorOnlyProgress = t;
#endif
        }

        public void SetFromAsTargetValueSafe() {
            if(Target == null) {
                Debug.LogErrorFormat($"Target is null. Please assign a target first.", Target);
                return;
            }
            SetFromAsTargetValue();
        }
        public void SetToAsTargetValueSafe() {
            if(Target == null) {
                Debug.LogErrorFormat($"Target is null. Please assign a target first.", Target);
                return;
            }
            SetToAsTargetValue();
        }
        
        /// <summary>
        /// Override this to capture the target's current value and set it as the FROM value.
        /// Called when user clicks "Set From" button in inspector.
        /// </summary>
        public virtual void SetFromAsTargetValue(){}
        
        /// <summary>
        /// Override this to capture the target's current value and set it as the TO value.
        /// Called when user clicks "Set To" button in inspector.
        /// </summary>
        public virtual void SetToAsTargetValue(){}

        public void OnSequenceReversed() {
            if (typeof(UFrom) == typeof(VTo))
            {
                object temp = From;
                From = (UFrom)(object)To;
                To = (VTo)temp;
            }
        }

        public override string GetDisplayName(){
            if(Target == null) return $"[null] [{Duration}s]";
            Component c = Target as Component;
            if(c != null) return $"[{c.gameObject.name}] [{Duration}s]";

            UnityEngine.Object o = Target as UnityEngine.Object;
            if(o != null) return $"[{o.name}] [{Duration}s]";

            return $"[{Duration}s]";
        }

        protected Func<UFrom, VTo, float, WInterpolationOutput> _lerpFunction;
        protected Tween() {
            _lerpFunction = InterpolationFunction;
        }
        
        /// <summary>
        /// Override this to define the interpolation function.
        /// Should return a Func that takes (from, to, normalizedTime) and returns the interpolated value.
        /// </summary>
        /// <example>
        /// protected override Func&lt;Vector3, Vector3, float, Vector3&gt; InterpolationFunction => Vector3.LerpUnclamped;
        /// </example>
        protected abstract Func<UFrom, VTo, float, WInterpolationOutput> InterpolationFunction {get;}
        
        /// <summary>
        /// Called when a target is assigned to this tween.
        /// Default implementation calls SetFromAsTargetValue() and SetToAsTargetValue().
        /// </summary>
        public virtual void OnTargetAssigned() {
            SetFromAsTargetValue();
            SetToAsTargetValue();
        }


        public Tween<TComponent, UFrom, VTo, WInterpolationOutput> SetDuration(float duration) {
            Duration = duration;
            return this;
        }
        public Tween<TComponent, UFrom, VTo, WInterpolationOutput> SetEase(Ease easeType){
            EaseType = easeType;
            return this;
        }
        public Tween<TComponent, UFrom, VTo, WInterpolationOutput> SetFrom(UFrom from) {
            From = from;
            return this;
        }
        public Tween<TComponent, UFrom, VTo, WInterpolationOutput> SetTo(VTo to) {
            To = to;
            return this;
        }
        public Tween<TComponent, UFrom, VTo, WInterpolationOutput> SetTarget(TComponent target) {
            Target = target;
            return this;
        }
        public Tween<TComponent, UFrom, VTo, WInterpolationOutput> SetParam(UFrom from, VTo to, float duration, Ease easeType) {
            From = from;
            To = to;
            Duration = duration;
            EaseType = easeType;
            return this;
        }
    }
}
