using UnityEngine;
using UnityEditor;

namespace DhafinFawwaz.AnimationUI
{
    public class AnimationUICustomMenu
    {
        [MenuItem("GameObject/UI (Canvas)/Create AnimationUI")]
        static void CreateAnimationUI(MenuCommand menuCommand)
        {
            GameObject selected = Selection.activeGameObject;
            GameObject createdGo = new GameObject("AnimationUI");
            var aui = createdGo.AddComponent<AnimationUI>();
            aui.Sequence.Add(new PositionTween());
            
            GameObjectUtility.SetParentAndAlign(createdGo, selected);
            Undo.RegisterCreatedObjectUndo(createdGo, "Created +"+createdGo.name);
        }

    }

}