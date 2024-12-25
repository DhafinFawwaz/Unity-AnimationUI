using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {

    // interface for component that make the AnimationUI wait for a certain duration
    public interface IWaitable
    {
        public float GetDuration();
    }
}
