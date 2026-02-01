using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    // Just to group all Tween with Image
    public abstract class ImageTween<T, U, V, W> : Tween<T, U, V, W> where T : Component
    {}
}
