using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {
    // interface for component that will have a target with tweenable value with from and to value
    public interface ITweenable
    {
        public void SetTarget(UnityEngine.Object target);
        public UnityEngine.Object GetTarget();
        // public virtual void OnTargetAssigned(){
        //     SetTargetValueAsFromSafe();
        //     SetTargetValueAsToSafe();
        // }


        public float GetDuration();
        public void Update(float time);
        public void UpdateToFrom();
        public void UpdateToTo();
        public abstract void SetFromAsTargetValueSafe();
        public abstract void SetToAsTargetValueSafe();
    }
}
