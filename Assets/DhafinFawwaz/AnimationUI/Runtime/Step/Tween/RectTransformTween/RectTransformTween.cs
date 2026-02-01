using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    // Just to group all Tween with RectTransform
    public abstract class RectTransformTween<T, U, V, W> : Tween<T, U, V, W> where T : Component
    {}
}
