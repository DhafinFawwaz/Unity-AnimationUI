using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public abstract class MeshTween<T, U, V, W> : Tween<T, U, V, W> where T : UnityEngine.Object
    {}
}
