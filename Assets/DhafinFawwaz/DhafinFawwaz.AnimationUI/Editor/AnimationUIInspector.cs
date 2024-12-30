using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
namespace DhafinFawwaz.AnimationUI {
    [CustomEditor(typeof(AnimationUI))]
    public class AnimationUIInspector : Editor
    {
        AnimationUI _target;
        GUIStyle redButton;

        void OnEnable() {
            _target = target as AnimationUI;
            GUIStyle redButton = new GUIStyle(GUI.skin.button);
            redButton.normal.background = Create1pxTexture(new Color(1,0,0,0.3f));
        }

        Texture2D Create1pxTexture(Color color) {
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color);
            tex.Apply();
            return tex;
        }

        void SetInspectorLock(bool locked) {
            var inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
            var inspectorWindow = EditorWindow.GetWindow(inspectorType);
            if (inspectorWindow != null) {
                var isLockedProperty = inspectorType.GetProperty("isLocked", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                if (isLockedProperty != null)
                {
                    isLockedProperty.SetValue(inspectorWindow, locked, null);
                }
            }
        }
        bool IsInspectorLocked() {
            var inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
            var inspectorWindow = EditorWindow.GetWindow(inspectorType);
            if (inspectorWindow != null) {
                var isLockedProperty = inspectorType.GetProperty("isLocked", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                if (isLockedProperty != null)
                {
                    return (bool)isLockedProperty.GetValue(inspectorWindow, null);
                }
            }
            return false;
        }

        public override void OnInspectorGUI() {
            var it = serializedObject.GetIterator();
            it.NextVisible(true); // enter children

            // EditorGUILayout.PropertyField(it, true); // Skip script field
            it.NextVisible(false);

            

            // Play Button
            float totalDuration = _target.CalculateTotalDuration();
            if(_target.CurrentAnimationTime == 0 && !_target.IsPlaying && GUILayout.Button("Play")) {
                _target.Play();
            } else if(_target.CurrentAnimationTime > 0 && _target.CurrentAnimationTime < totalDuration && !_target.IsPlaying && GUILayout.Button("Resume")) {
                _target.Resume();
            } else if(_target.IsPlaying && _target.CurrentAnimationTime < totalDuration && GUILayout.Button("Pause", redButton)) {
                _target.Pause();
            } else if(!_target.IsPlaying && _target.CurrentAnimationTime >= totalDuration && GUILayout.Button("Replay")) {
                _target.Play();
            }

            // Preview Start | Preview End
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Preview Start")) {
                _target.CurrentAnimationTime = 0;
                _target.ApplyAllAtTime(0);
            }
            if(GUILayout.Button("Preview End")) {
                _target.CurrentAnimationTime = totalDuration;
                _target.ApplyAllAtTime(totalDuration);
            }
            GUILayout.EndHorizontal();


            // Slider
            if(!_target.IsPlaying) {
                float sliderValue = GUILayout.HorizontalSlider(_target.CurrentAnimationTime, 0, totalDuration, GUILayout.ExpandWidth(true), GUILayout.Height(20));
                if(sliderValue != _target.CurrentAnimationTime) //Happens when dragging progess bar
                {
                    _target.CurrentAnimationTime = sliderValue;
                    _target.ApplyAllAtTime(_target.CurrentAnimationTime);
                }
            } else {
                GUILayout.HorizontalSlider(_target.CurrentAnimationTime, 0, totalDuration, GUILayout.ExpandWidth(true), GUILayout.Height(20));
            }


            Color defaultColor = GUI.backgroundColor;
            GUI.backgroundColor = new Color(0.2f, 1, 0.2f);
            Rect position = GUILayoutUtility.GetLastRect();
            EditorGUI.ProgressBar(position, _target.CurrentAnimationTime/totalDuration, 
                Mathf.Clamp(Mathf.Round(_target.CurrentAnimationTime*100)/100, 0, 100)
                    .ToString()+"/"+totalDuration.ToString()+" Seconds, ["+(Mathf.Round(_target.CurrentAnimationTime/totalDuration*10000)/100)
                    .ToString()+"%]"
            );
            GUI.backgroundColor = defaultColor;

            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(it, true); // Play on Start
            bool _isInspectorLocked = IsInspectorLocked();
            bool newLockState = GUILayout.Toggle(_isInspectorLocked, _isInspectorLocked ? "Locked" : "Unlocked", "Button", GUILayout.Width(100));
            if (newLockState != _isInspectorLocked) {
                SetInspectorLock(newLockState);
            }
            EditorGUILayout.EndHorizontal();

            it.NextVisible(false);
            EditorGUILayout.PropertyField(it, true);


            if(GUILayout.Button("Reverse Sequence")) {
                _target.ReverseSequence();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
