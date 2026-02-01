using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    /// <summary>
    /// A step that loads a scene using SceneManager.LoadScene().
    /// Only executes in play mode.
    /// </summary>
    [Serializable, BGColor("#964B0015")]
    public class LoadScene : Step, IExecutable
    {
        /// <summary>
        /// The name of the scene to load.
        /// </summary>
        public string SceneName = "SceneName";
#if UNITY_EDITOR
        LoadScene(){
            UnityEditor.EditorApplication.delayCall += () => {
                if(UnityEngine.SceneManagement.SceneManager.sceneCount == 0) return;
                if(SceneName == "SceneName") SceneName = UnityEngine.SceneManagement.SceneManager.GetSceneAt(0).name;
            };
        }
#endif

        public void Execute(){
#if UNITY_EDITOR
            if(Application.isPlaying)
#endif
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
            
            EditorOnlyProgress = 1;
        }

        public void Dexecute(){
            // nothing
            EditorOnlyProgress = 0;
        }

        public override string GetDisplayName(){
            return $"[{SceneName}]";
        }
    }
}
