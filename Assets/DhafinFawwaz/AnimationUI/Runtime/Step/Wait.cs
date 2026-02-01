using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    /// <summary>
    /// A step that introduces a delay in the animation sequence.
    /// All tweens before a Wait step will execute simultaneously, then the animation waits for the specified duration.
    /// </summary>
    [Serializable] 
    [BGColor("#0000ff15")]
    public class Wait : Step, IWaitable
    {
        /// <summary>
        /// The duration of the wait in seconds.
        /// </summary>
        public float Duration = 0.5f;
        public float GetDuration(){
            return Duration;
        }
        public override string GetDisplayName(){
            return $"[{Duration}s]";
        }
    }
}
