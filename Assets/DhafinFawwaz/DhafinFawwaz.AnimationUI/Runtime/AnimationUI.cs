using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace DhafinFawwaz.AnimationUI {

    [ExecuteAlways]
    public class AnimationUI : MonoBehaviour
    {

#if UNITY_EDITOR
        bool AutoAssign<T, U>(int idx, Automatic automatic) where T : UnityEngine.Object where U : Step, ITweenable, new() {
            Component c = automatic.AutomaticTarget as Component;
            if(c != null && c.TryGetComponent(out T t1)) {
                _sequence[idx] = new U();
                ITweenable tweenable = _sequence[idx] as ITweenable;
                tweenable.SetTarget(t1);
                return true;
            } else if(automatic.AutomaticTarget is T t2) {
                _sequence[idx] = new U();
                ITweenable tweenable = _sequence[idx] as ITweenable;
                tweenable.SetTarget(t2);
                return true;
            }

            return false;
        }

        // Dictionary<Step, bool> IsTargetAssigned = new();
        void OnValidate() {
            for(int i = 0; i < _sequence.Count; i++) {
                if(_sequence[i] == null) _sequence[i] = new Automatic();
                else if(_sequence[i] is Automatic a && a.AutomaticTarget != null) {
                    if(AutoAssign<AudioSource, VolumeTween>(i, a)) continue;
                    else if(AutoAssign<AudioMixer, MixerTween>(i, a)) continue;
                    else if(AutoAssign<Camera, OrthographicSizeTween>(i, a)) continue;
                    else if(AutoAssign<CanvasGroup, AlphaTween>(i, a)) continue;
                    else if(AutoAssign<UnityEngine.UI.Image, ImageColorTween>(i, a)) continue;
                    else if(AutoAssign<Material, MaterialColorTween>(i, a)) continue;
                    else if(AutoAssign<SpriteRenderer, SpriteColorTween>(i, a)) continue;
                    else if(AutoAssign<TMP_Text, TextRevealTween>(i, a)) continue;
                    else if(AutoAssign<RectTransform, AnchoredPositionTween>(i, a)) continue;
                    else if(AutoAssign<Transform, PositionTween>(i, a)) continue;
                }
            }


            // Set the Name
            float currentTime = 0;
            for(int i = 0; i < _sequence.Count; i++) {
                _sequence[i].EditorOnlyName = $"{currentTime.ToString("0.00")} s";
                var wait = _sequence[i] as IWaitable;
                if(wait != null) currentTime += wait.GetDuration();
                
                var tweenable = _sequence[i] as Step;
                if(tweenable != null) {
                    _sequence[i].EditorOnlyName += $" {tweenable.GetDisplayName()}";
                }
                
            }

            // // Sync the IsTargetAssigned
            // foreach(var step in _sequence) {
            //     if(!IsTargetAssigned.ContainsKey(step)) IsTargetAssigned[step] = false;
            // }
            // LinkedList<Step> toRemove = new();
            // foreach(var kvp in IsTargetAssigned) {
            //     if(!_sequence.Contains(kvp.Key)) toRemove.AddLast(kvp.Key);
            // }
            // foreach(var step in toRemove) IsTargetAssigned.Remove(step);
            // // Call OnTargetAssigned() when the target is assigned (false in IsTargetAssigned, not null in _sequence)
            // foreach(var step in _sequence) {
            //     if(!IsTargetAssigned[step] && step is ITweenable tweenable && tweenable.GetTarget() != null) {
            //         tweenable.OnTargetAssigned();
            //         IsTargetAssigned[step] = true;
            //     }
            // }

        }

        void ForceRepaint() {
            UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
            UnityEditor.SceneView.RepaintAll();
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }
        // void OnDrawGizmos() {
            
        // }

        void EditorUpdateLoop() {
            if(IsPlaying) {
                ForceRepaint();
            }
            UpdateLoop();
        }

#endif

        public List<Step> Sequence => _sequence;
        public T Get<T>(int idx) where T : Step => _sequence[idx] as T;

        public bool IsNotPlaying => _state == AnimationUIState.IsNotPlaying;
        public bool IsPlaying => _state == AnimationUIState.IsPlaying; // Cannot use CurrentAnimationTime > 0 because it will be true even if the animation is finished. So we use AnimationUIState instead
        public bool IsPaused => _state == AnimationUIState.IsPaused;


        public bool PlayOnStart = false;
        [HideInInspector] public float CurrentAnimationTime = 0;
        float _startPlayTime = 0;
        [SerializeReference, SubclassSelectorAnimationUI] List<Step> _sequence = new List<Step>();
        AnimationUIState _state = AnimationUIState.IsNotPlaying;

        
        void Start() {
#if UNITY_EDITOR
            if(Application.isPlaying)
#endif
            if(PlayOnStart) Play();
        }

        void SubscribeUpdateLoop() {
#if UNITY_EDITOR
            if(!Application.IsPlaying(gameObject)) {
                UnityEditor.EditorApplication.update += EditorUpdateLoop;
            } else {
                AnimationUIRunner.Instance.Tweenables += UpdateLoop;
            } 
#else
            AnimationUIRunner.Instance.Tweenables += UpdateLoop;
#endif
        }

        void UnsubscribeUpdateLoop() {
#if UNITY_EDITOR
                if(!Application.IsPlaying(gameObject)) {
                    UnityEditor.EditorApplication.update -= EditorUpdateLoop;
                } else {
                    AnimationUIRunner.Instance.Tweenables -= UpdateLoop;
                } 
#else
                AnimationUIRunner.Instance.Tweenables -= UpdateLoop;
#endif
        }

        public void Play() {
#if UNITY_EDITOR
            // if(gameObject.activeInHierarchy == false) {
            //     Debug.LogWarning("AnimationUI is not playing because the GameObject is not active in hierarchy");
            //     return;
            // }
            // if(!enabled) {
            //     Debug.LogWarning("AnimationUI is not playing because the component is disabled");
            //     return;
            // }
            if(_sequence.Count == 0) {
                Debug.LogWarning("AnimationUI is not playing because the sequence is empty");
                return;
            }
#endif
            CurrentAnimationTime = 0;
            _startPlayTime = Time.realtimeSinceStartup;
            _state = AnimationUIState.IsPlaying;

            _wait = null;
            _tweenableDict = new();
            _stepIndex = 0;
            _waitUntilTime = 0;
            _tweenableDict[_waitUntilTime] = new LinkedList<ITweenable>();

            
            SubscribeUpdateLoop();
        }

        public void Pause() {
            _state = AnimationUIState.IsPaused;
            UnsubscribeUpdateLoop();
        }
        public void Resume() {
            if(!_tweenableDict.ContainsKey(_waitUntilTime)) _tweenableDict[_waitUntilTime] = new LinkedList<ITweenable>();
            // CurrentAnimationTime is set by other script or from inspector
            _startPlayTime = Time.realtimeSinceStartup - CurrentAnimationTime;
            _state = AnimationUIState.IsPlaying;

            SubscribeUpdateLoop();


            _wait = null;
            _tweenableDict = new();
            _stepIndex = 0;
            _waitUntilTime = 0;
            _tweenableDict[_waitUntilTime] = new LinkedList<ITweenable>();
            // Collect all until time is greater than CurrentAnimationTime
            while(_stepIndex < _sequence.Count) {
                var tweenable = _sequence[_stepIndex] as ITweenable;
                if(tweenable != null) _tweenableDict[_waitUntilTime].AddLast(tweenable);

                _wait = _sequence[_stepIndex] as IWaitable;
                if(_wait != null) {
                    _waitUntilTime += _wait.GetDuration();
                    _tweenableDict[_waitUntilTime] = new LinkedList<ITweenable>();
                    _stepIndex++;
                    if(CurrentAnimationTime < _waitUntilTime) break;
                    else continue;
                }

                _stepIndex++;
            }

            // Run all
            foreach(var kvp in _tweenableDict) {
                var linkedList = kvp.Value;
                var node = linkedList.First;
                var startTime = kvp.Key;
                while(node != null) {
                    var endTime = startTime + node.Value.GetDuration();
                    var nextNode = node.Next; // Store next node before current node is removed
                    
                    if(CurrentAnimationTime < startTime) {
                        node.Value.UpdateToFrom();
                    }
                    else if(CurrentAnimationTime < endTime) {
                        node.Value.Update(CurrentAnimationTime - startTime);
                    }
                    else {
                        node.Value.UpdateToTo();
                        linkedList.Remove(node);
                    }

                    node = nextNode;
                }
            }
        }

        public void ReverseSequence() {
            _sequence.Reverse();
            foreach(var step in _sequence) {
                var handler = step as IReverseSequenceHandler;
                if(handler != null) handler.OnSequenceReversed();
            }
        }

        
        IWaitable _wait = null;
        Dictionary<float, LinkedList<ITweenable>> _tweenableDict = new();
        int _stepIndex = 0;
        float _waitUntilTime = 0;


        public void UpdateLoop() {
            // CurrentAnimationTime += Time.deltaTime;
            CurrentAnimationTime = Time.realtimeSinceStartup - _startPlayTime;

            // Run all
            foreach(var kvp in _tweenableDict) {
                var linkedList = kvp.Value;
                var node = linkedList.First;
                var startTime = kvp.Key;
                while(node != null) {
                    var endTime = startTime + node.Value.GetDuration();
                    var nextNode = node.Next; // Store next node before it's removed
                    
                    if(CurrentAnimationTime < startTime) {
                        node.Value.UpdateToFrom();
                    }
                    else if(CurrentAnimationTime < endTime) {
                        node.Value.Update(CurrentAnimationTime - startTime);
                    }
                    else {
                        node.Value.UpdateToTo();
                        linkedList.Remove(node);
                    }

                    node = nextNode;
                }
            }

#if UNITY_EDITOR
            var castStep = _wait as Step;
            if(castStep != null) castStep.EditorOnlyProgress = Mathf.Clamp01(1 - (_waitUntilTime - CurrentAnimationTime) / _wait.GetDuration());
#endif
            if(CurrentAnimationTime < _waitUntilTime) return; // wait

            // Collect all until wait
            while(_stepIndex < _sequence.Count) {
                var tweenable = _sequence[_stepIndex] as ITweenable;
                if(tweenable != null) _tweenableDict[_waitUntilTime].AddLast(tweenable);

                _wait = _sequence[_stepIndex] as IWaitable;
                if(_wait != null) {
                    _waitUntilTime += _wait.GetDuration();
                    _tweenableDict[_waitUntilTime] = new LinkedList<ITweenable>();
                    _stepIndex++;
                    break;
                }

                var executable = _sequence[_stepIndex] as IExecutable;
                if(executable != null) {
                    executable.Execute();
                    _stepIndex++;
                    continue;
                }


                _stepIndex++;
            }


            bool isAllTweenableDictEmpty() {
                foreach(var kvp in _tweenableDict) {
                    if(kvp.Value.Count > 0) return false;
                }
                return true;
            }
            if(_stepIndex == _sequence.Count && isAllTweenableDictEmpty()) {
                _state = AnimationUIState.IsNotPlaying;
#if UNITY_EDITOR
                CurrentAnimationTime = CalculateTotalDuration();
#endif
                UnsubscribeUpdateLoop();
            } else if(_stepIndex > _sequence.Count) {
                _state = AnimationUIState.IsNotPlaying;
#if UNITY_EDITOR
                CurrentAnimationTime = CalculateTotalDuration();
#endif
                UnsubscribeUpdateLoop();

                Debug.LogWarning("For some reason _stepIndex > _sequence.Count. Might be a bug.");

            }
        }

        public float CalculateTotalDuration() {
            float max = 0;
            float currentTime = 0;
            for(int i = 0; i < _sequence.Count; i++) {
                var tweenable = _sequence[i] as ITweenable;
                if(tweenable != null) {
                    max = Mathf.Max(max, tweenable.GetDuration() + currentTime);
                }

                var wait = _sequence[i] as IWaitable;
                if(wait != null) {
                    max = Mathf.Max(max, wait.GetDuration() + currentTime);
                    currentTime += wait.GetDuration();
                }
            }
            return max;
        }

        public void ApplyAllAtTime(float t) {
            // Iterate from start to current, get the index.
            float currentTime = 0;
            int currentIdx;
            for(currentIdx = 0; currentIdx < _sequence.Count; currentIdx++) {
                var wait = _sequence[currentIdx] as IWaitable;
                if(wait != null) {
#if UNITY_EDITOR
                    var step = _sequence[currentIdx];
                    if(step != null) step.EditorOnlyProgress = Mathf.Clamp01((t - currentTime) / wait.GetDuration());
#endif
                    currentTime += wait.GetDuration();
                    if(currentTime > t) {
                        break;
                    }
                    continue;
                }
            }

            
            // Iterate from end to current while also calling UpdateToFrom. This bassically set every step after current to start state
            currentTime = 0;
            for(int i = _sequence.Count-1; i > currentIdx; i--) { // Exclude currentIdx
#if UNITY_EDITOR
                var wait = _sequence[i] as IWaitable;
                if(wait != null) {
                    var step = _sequence[i];
                    if(step != null) step.EditorOnlyProgress = 0;
                }
#endif

                var executable = _sequence[i] as IExecutable;
                if(executable != null) {
                    executable.Dexecute();
                    continue;
                }

                var tweenable = _sequence[i] as ITweenable;
                if(tweenable != null) {
                    tweenable.UpdateToFrom();
                }
            }


            // Iterate from start to current again while also calling either UpdateToFrom, Update, or UpdateToTo
            currentTime = 0;
            for(int i = 0; i < currentIdx; i++) {
                var wait = _sequence[i] as IWaitable;
                if(wait != null) {
                    currentTime += wait.GetDuration();
                    continue;
                }

                var executable = _sequence[i] as IExecutable;
                if(executable != null) {
                    if(t >= currentTime) executable.Execute();
                    else executable.Dexecute();
                    continue;
                }

                var tweenable = _sequence[i] as ITweenable;
                if(tweenable != null) {
                    if(t < currentTime) tweenable.UpdateToFrom();
                    else if(t < currentTime + tweenable.GetDuration()) {
                        tweenable.Update(t-currentTime);
                    }
                    else tweenable.UpdateToTo();
                    continue;
                }
            }

        }
    }

}
