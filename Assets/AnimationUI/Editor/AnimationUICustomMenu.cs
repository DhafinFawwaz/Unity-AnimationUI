using UnityEngine;
using UnityEditor;

namespace DhafinFawwaz.AnimationUILib.EditorLib
{
public class AnimationUICustomMenu
{
    [MenuItem("GameObject/UI/Create AnimationUI")]
    static void CreateAnimationUI(MenuCommand menuCommand)
    {
        GameObject selected = Selection.activeGameObject;
        GameObject createdGo = new GameObject("AnimationUI");
        createdGo.AddComponent<AnimationUI>();
        GameObjectUtility.SetParentAndAlign(createdGo, selected);
        Undo.RegisterCreatedObjectUndo(createdGo, "Created +"+createdGo.name);
    }

}

}