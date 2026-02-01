using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    /// <summary>
    /// A placeholder step that gets automatically converted to the appropriate tween type.
    /// When you drag a component into this step, AnimationUI detects the type and creates the matching tween.
    /// </summary>
    [Serializable]
    public class Automatic : Step
    {
        /// <summary>
        /// The component or GameObject to automatically detect and create a tween for.
        /// </summary>
        public UnityEngine.Object AutomaticTarget;
    }
}
