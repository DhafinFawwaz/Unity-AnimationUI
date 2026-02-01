using System;
using UnityEngine;
using UnityEngine.Audio;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    // Just to group all Tween with AudioSource
    public abstract class AudioSourceTween<T, U, V, W> : Tween<T, U, V, W> where T : UnityEngine.Object
    {}
}
