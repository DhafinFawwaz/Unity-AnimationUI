using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    // Just to group all Tween with Camera
    public abstract class CameraTween<T, U, V, W> : Tween<T, U, V, W> where T : Component
    {}
}
