using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {

    /// <summary>
    /// Interface for steps that introduce a delay/wait in the animation sequence.
    /// Implement this for custom wait or timing-related steps.
    /// </summary>
    public interface IWaitable
    {
        /// <summary>
        /// Gets the duration of the wait in seconds.
        /// </summary>
        /// <returns>Duration in seconds</returns>
        public float GetDuration();
    }
}
