using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace DhafinFawwaz.AnimationUILib.Demo
{

[CustomEditor(typeof(AudioManager))]
public class AudioManagerInspector : Editor
{
    AudioManager _script;

    void OnEnable()
    {
        _script = (AudioManager)target;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        int i = 0;
        int j = 0;
        int width = 100;
        while(i < _script.SFX.Length)
        {
            j = 0;
            EditorGUILayout.BeginHorizontal();
            do
            {
                var nameProperty = this.serializedObject.FindProperty("SFX").
                    GetArrayElementAtIndex(i).FindPropertyRelative("ClipName");
                var clipProperty = this.serializedObject.FindProperty("SFX").
                    GetArrayElementAtIndex(i).FindPropertyRelative("Clip");
                var volumeProperty = this.serializedObject.FindProperty("SFX").
                    GetArrayElementAtIndex(i).FindPropertyRelative("Volume");
                
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(20));
                EditorGUILayout.PropertyField(nameProperty, GUIContent.none, GUILayout.Width(75));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(clipProperty, GUIContent.none, GUILayout.Width(75));
                EditorGUILayout.PropertyField(volumeProperty, GUIContent.none, GUILayout.Width(20));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();

                EditorGUILayout.Space();
                i++;
                j++;
            }
            while((j+1)*width < EditorGUIUtility.currentViewWidth && i < _script.SFX.Length);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

        }


            GUILayout.FlexibleSpace();

        serializedObject.ApplyModifiedProperties();

    }
}

}