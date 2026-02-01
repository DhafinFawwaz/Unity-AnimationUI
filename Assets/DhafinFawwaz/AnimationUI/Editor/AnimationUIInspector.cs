using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
namespace DhafinFawwaz.AnimationUI {
    [CustomEditor(typeof(AnimationUI))]
    public class AnimationUIInspector : Editor
    {
        AnimationUI _target;

        void OnEnable() {
            _target = target as AnimationUI;
        }

        Texture2D Create1pxTexture(Color color) {
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color);
            tex.Apply();
            return tex;
        }

        void SetInspectorLock(bool locked) {
            ActiveEditorTracker.sharedTracker.isLocked = locked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }
        bool IsInspectorLocked() {
            return ActiveEditorTracker.sharedTracker.isLocked;
        }

        public override void OnInspectorGUI() {
            var it = serializedObject.GetIterator();
            it.NextVisible(true); // enter children

            // EditorGUILayout.PropertyField(it, true); // Skip script field
            it.NextVisible(false);

            

            // Play Button
            float totalDuration = _target.CalculateTotalDuration();
            if(_target.Sequence.Count == 0) {
                if(GUILayout.Button("Play")) _target.Play(); // If i put GUILayout.Button("Play") in the previous if statement with &&, it will show 2 Play button and 1 Replay Button. Don't complain about this.
            }
            else if(_target.CurrentAnimationTime == 0 && !_target.IsPlaying && GUILayout.Button("Play")) {
                _target.Play();
            } else if(_target.CurrentAnimationTime > 0 && _target.CurrentAnimationTime < totalDuration && !_target.IsPlaying && GUILayout.Button("Resume")) {
                _target.Resume();
            } else if(_target.IsPlaying && _target.CurrentAnimationTime < totalDuration ) {
                Color temp = GUI.backgroundColor;
                GUI.backgroundColor = Color.red;
                if(GUILayout.Button("Pause")) _target.Pause();
                GUI.backgroundColor = temp;
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
            bool newLockState = GUILayout.Toggle(_isInspectorLocked, _isInspectorLocked ? "Locked" : "Unlocked", "Button", GUILayout.Width(70));
            if (newLockState != _isInspectorLocked) {
                SetInspectorLock(newLockState);
                return; // must return because it.NextVisible(false) will throw an error saying that property is disposed
            }
            EditorGUILayout.EndHorizontal();
            
            
            it.NextVisible(false);
            EditorGUILayout.PropertyField(it, true); // Ignore Time Scale
            

            it.NextVisible(false);
            EditorGUILayout.PropertyField(it, true); // Sequence


            if(GUILayout.Button("Reverse Sequence")) {
                _target.ReverseSequence();
            }

            serializedObject.ApplyModifiedProperties();

        }
    }
}
