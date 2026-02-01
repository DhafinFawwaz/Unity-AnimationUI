using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    // Just to group all Tween with Material
    public abstract class MaterialTween<T, U, V, W> : Tween<T, U, V, W> where T : UnityEngine.Object
    {}
}
