using System;
using UnityEngine;
namespace DhafinFawwaz.AnimationUI {
    // Singleton
    public class AnimationUIRunner : MonoBehaviour
    {

        static AnimationUIRunner _instance;
        public static AnimationUIRunner Instance => _instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoad()
        {
            if(_instance != null) return;
            _instance = new GameObject("AnimationUIRunner").AddComponent<AnimationUIRunner>();
            DontDestroyOnLoad(_instance.gameObject);
        }


        public Action Tweenables;

        void Update() {
            Tweenables?.Invoke();
        }
    }
}