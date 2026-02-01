using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {

    /// <summary>
    /// Interface for steps that need a reference to the parent AnimationUI or other injected objects.
    /// The AnimationUI will automatically call Inject() during OnValidate() in the editor.
    /// </summary>
    public interface IInjectable
    {
        /// <summary>
        /// Called automatically by AnimationUI to inject the parent reference.
        /// Typically used to set a private AnimationUI field for accessing the sequence or other properties.
        /// </summary>
        /// <param name="injected">The object being injected (usually the parent AnimationUI component)</param>
        public void Inject(object injected);
    }
}
