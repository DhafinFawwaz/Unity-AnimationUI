using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {
    /// <summary>
    /// Interface for steps that need special behavior when the sequence is reversed.
    /// Implement this to handle sequence reversal (e.g., swapping From/To values, toggling booleans).
    /// </summary>
    public interface IReverseSequenceHandler
    {
        /// <summary>
        /// Called when AnimationUI.ReverseSequence() is invoked.
        /// Use this to swap values or adjust behavior for reverse playback.
        /// </summary>
        public void OnSequenceReversed();
    }
}
