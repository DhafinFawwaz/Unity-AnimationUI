using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    // Just to group all Tween with Sprite
    public abstract class SpriteTween<T, U, V, W> : Tween<T, U, V, W> where T : Component
    {}
}
