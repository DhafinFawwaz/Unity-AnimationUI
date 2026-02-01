using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {

    /// <summary>
    /// Interface for steps that execute an instant action (can be executed and reversed).
    /// Implement this for steps that perform one-shot actions like playing sounds, toggling objects, etc.
    /// </summary>
    public interface IExecutable
    {
        /// <summary>
        /// Executes the action (e.g., play sound, activate object).
        /// </summary>
        public void Execute();

        /// <summary>
        /// De-executes or reverses the action (e.g., deactivate object).
        /// Called when scrubbing backwards in the timeline.
        /// </summary>
        public void Dexecute();

    }
}
