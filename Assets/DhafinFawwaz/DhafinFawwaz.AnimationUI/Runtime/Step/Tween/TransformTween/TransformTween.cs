using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    // Just to group all Tween with Transform
    public abstract class TransformTween<T, U, V, W> : Tween<T, U, V, W> where T : Transform
    {}
}
