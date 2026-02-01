using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    // Just to group all Tween with CanvasGroup
    public abstract class CanvasGroupTween<T, U, V, W> : Tween<T, U, V, W> where T : Component
    {}
}
