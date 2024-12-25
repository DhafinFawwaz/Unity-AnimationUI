using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {
    // interface for component that will have a target with tweenable value with from and to value
    public interface ITweenable
    {
        public Component GetTarget();
        public float GetDuration();
        public void Update(float time);
        public void UpdateToFrom();
        public void UpdateToTo();
        public abstract void SetTargetValueAsFromSafe();
        public abstract void SetTargetValueAsToSafe();
    }
}
