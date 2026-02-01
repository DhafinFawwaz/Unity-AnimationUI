using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {
    /// <summary>
    /// Interface for steps that animate values over time with From and To values.
    /// Implement this for custom tweens that interpolate between two states.
    /// </summary>
    public interface ITweenable
    {
        /// <summary>
        /// Sets the target component that this tween will animate.
        /// </summary>
        /// <param name="target">The Unity object to animate</param>
        public void SetTarget(UnityEngine.Object target);
        
        /// <summary>
        /// Gets the current target component being animated.
        /// </summary>
        /// <returns>The Unity object being animated</returns>
        public UnityEngine.Object GetTarget();
        // public virtual void OnTargetAssigned(){
        //     SetTargetValueAsFromSafe();
        //     SetTargetValueAsToSafe();
        // }


        /// <summary>
        /// Gets the duration of this tween in seconds.
        /// </summary>
        /// <returns>Duration in seconds</returns>
        public float GetDuration();
        
        /// <summary>
        /// Updates the tween to the state at the specified time.
        /// </summary>
        /// <param name="time">The time position in seconds</param>
        public void Update(float time);
        
        /// <summary>
        /// Instantly sets the tween to its FROM state (start value).
        /// </summary>
        public void UpdateToFrom();
        
        /// <summary>
        /// Instantly sets the tween to its TO state (end value).
        /// </summary>
        public void UpdateToTo();
        
        /// <summary>
        /// Captures the target's current value and sets it as the FROM value.
        /// Called when the user clicks "Set From" button in inspector.
        /// </summary>
        public abstract void SetFromAsTargetValueSafe();
        
        /// <summary>
        /// Captures the target's current value and sets it as the TO value.
        /// Called when the user clicks "Set To" button in inspector.
        /// </summary>
        public abstract void SetToAsTargetValueSafe();
    }
}
