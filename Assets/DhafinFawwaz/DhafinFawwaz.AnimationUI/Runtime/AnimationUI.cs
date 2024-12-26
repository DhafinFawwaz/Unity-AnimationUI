using System.Collections.Generic;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [ExecuteAlways]
    public class AnimationUI : MonoBehaviour
    {

#if UNITY_EDITOR
        void OnValidate() {
            for(int i = 0; i < _sequence.Count; i++) {
                if(_sequence[i] == null) _sequence[i] = new Automatic();
                else if(_sequence[i] is Automatic automatic && automatic.AutomaticTarget != null) {
                    if(automatic.AutomaticTarget.TryGetComponent(out Camera camera)) _sequence[i] = new OrthographicSizeTween() { Target = camera };
                    else if(automatic.AutomaticTarget.TryGetComponent(out Transform transform)) _sequence[i] = new PositionTween() { Target = transform };
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
        void OnEnable() {
#if UNITY_EDITOR
            if(!Application.IsPlaying(gameObject)) {
                UnityEditor.EditorApplication.update += EditorUpdateLoop;
            }
#endif
#if UNITY_EDITOR
            if(Application.IsPlaying(gameObject)) {
#endif
                AnimationUIRunner.Instance.Tweenables += UpdateLoop;
#if UNITY_EDITOR
            } 
#endif
        }
        void OnDisable() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.update -= EditorUpdateLoop;
#endif
#if UNITY_EDITOR
            if(Application.IsPlaying(gameObject)) {
#endif
                AnimationUIRunner.Instance.Tweenables -= UpdateLoop;
#if UNITY_EDITOR
            }
#endif
        }

        public bool PlayOnStart = false;
        void Start() {
#if UNITY_EDITOR
            if(Application.isPlaying)
#endif
            if(PlayOnStart) Play();
        }
        [SerializeReference, SubclassSelectorAnimationUI] List<Step> _sequence = new List<Step>();
        public List<Step> Sequence => _sequence;

        public bool IsPlaying => _isPlaying;
        bool _isPlaying;

        [HideInInspector]
        public float CurrentAnimationTime;     
        float _startPlayTime; 
        public void Play() {
#if UNITY_EDITOR
            if(gameObject.activeInHierarchy == false) {
                Debug.LogWarning("AnimationUI is not playing because the GameObject is not active in hierarchy");
                return;
            }
            if(!enabled) {
                Debug.LogWarning("AnimationUI is not playing because the component is disabled");
                return;
            }
            if(_sequence.Count == 0) {
                Debug.LogWarning("AnimationUI is not playing because the sequence is empty");
                return;
            }
#endif

            _isPlaying = true;
            CurrentAnimationTime = 0;
            _startPlayTime = Time.realtimeSinceStartup;

            _wait = null;
            _tweenableDict = new();
            _stepIndex = 0;
            _waitUntilTime = 0;
            _tweenableDict[_waitUntilTime] = new LinkedList<ITweenable>();
        }

        public void Pause() {
            _isPlaying = false;
        }
        public void Resume() {
            if(!_tweenableDict.ContainsKey(0)) _tweenableDict[_waitUntilTime] = new LinkedList<ITweenable>();
            _startPlayTime = Time.realtimeSinceStartup - CurrentAnimationTime;
            _isPlaying = true;
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

#if UNITY_EDITOR
        void Update() {
            if(!Application.IsPlaying(gameObject)) return;
            if(IsPlaying) ForceRepaint();
        }
#endif

        public void UpdateLoop() {
            if(!_isPlaying) return;
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

            // float totalDuration = CalculateTotalDuration();
            // if(_stepIndex == _sequence.Count && CurrentAnimationTime >= totalDuration) {
            //     _isPlaying = false;
            //     CurrentAnimationTime = totalDuration;
            // }

            bool isAllTweenableDictEmpty() {
                foreach(var kvp in _tweenableDict) {
                    if(kvp.Value.Count > 0) return false;
                }
                return true;
            }
            if(_stepIndex == _sequence.Count && isAllTweenableDictEmpty()) {
                _isPlaying = false;
#if UNITY_EDITOR
                CurrentAnimationTime = CalculateTotalDuration();
#endif
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
