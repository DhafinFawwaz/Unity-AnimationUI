using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    // Just to group all Tween with TextMeshPro
    public abstract class TMPTween<T, U, V, W> : Tween<T, U, V, W> where T : Component
    {}
}
