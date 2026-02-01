using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {

    /// <summary>
    /// Class that will interpolate a value from From to To and apply it to a certain object property.
    /// </summary>
    /// <typeparam name="TComponent">Component target that will have its property interpolated</typeparam>
    /// <typeparam name="UFrom">From value or component value</typeparam>
    /// <typeparam name="VTo">To value or component value</typeparam>
    /// <typeparam name="WInterpolationOutput">Interpolation function return type</typeparam>
    [BGColor("#ff000015")]
    public abstract class Tween<TComponent, UFrom, VTo, WInterpolationOutput> : Step, ITweenable, IReverseSequenceHandler where TComponent : UnityEngine.Object
    {
        public TComponent Target;
        public void SetTarget(UnityEngine.Object target) {
            Target = target as TComponent;
            OnTargetAssigned();
        }
        public UnityEngine.Object GetTarget() => Target;
        
        public float Duration = 0.5f;
        public float GetDuration() => Duration;
        public Ease EaseType = Ease.OutQuart;
        public UFrom From;
        public VTo To;
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
        
        public virtual void SetFromAsTargetValue(){}
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
        protected abstract Func<UFrom, VTo, float, WInterpolationOutput> InterpolationFunction {get;}
        
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
