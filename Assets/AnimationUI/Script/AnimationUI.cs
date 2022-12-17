using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class AnimationUI : MonoBehaviour
{
    public Sequence[] AnimationSequence;
    [HideInInspector] public bool PlayOnStart = false;
    void Start()
    {
#if UNITY_EDITOR
        if(Application.isPlaying)
#endif
        if(PlayOnStart)StartCoroutine(PlayAnimation());
    }
    public void Play() => StartCoroutine(PlayAnimation());
    public void PlayReversed() => StartCoroutine(PlayReversedAnimation());
    IEnumerator PlayAnimation()
    {
        Singleton.LoadSingleton();
        for(int i = 0; i < atTimeEvents.Count; i++)StartCoroutine(AtTimeEvent(atTimeEvents[i], atTimes[i])); //Function to call at time

        foreach(Sequence sequence in AnimationSequence)
        {
            if(sequence.SequenceType == Sequence.Type.Animation)
            {
                if(sequence.TargetComp == null)
                {
                    Debug.Log("Please assign Target for Sequence at "+sequence.StartTime.ToString()+"s");
                    continue;
                }
                
                if(sequence.TargetType == Sequence.ObjectType.RectTransform)
                {
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.AnchoredPosition))
                        StartCoroutine(TaskAnchoredPosition(sequence.TargetComp.GetComponent<RectTransform>(), 
                            sequence.AnchoredPositionStart, sequence.AnchoredPositionEnd, sequence.Duration, sequence.EaseFunction
                        ));
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.LocalEulerAngles))
                        StartCoroutine(TaskLocalEulerAngles(sequence.TargetComp.GetComponent<RectTransform>(), 
                            sequence.LocalEulerAnglesStart, sequence.LocalEulerAnglesEnd, sequence.Duration, sequence.EaseFunction
                        ));
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.LocalScale))
                        StartCoroutine(TaskLocalScale(sequence.TargetComp.GetComponent<RectTransform>(), 
                            sequence.LocalScaleStart, sequence.LocalScaleEnd, sequence.Duration, sequence.EaseFunction
                        ));
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.AnchorMax))
                        StartCoroutine(TaskAnchorMax(sequence.TargetComp.GetComponent<RectTransform>(), 
                            sequence.AnchorMaxStart, sequence.AnchorMaxEnd, sequence.Duration, sequence.EaseFunction
                        ));
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.AnchorMin))
                        StartCoroutine(TaskAnchorMin(sequence.TargetComp.GetComponent<RectTransform>(), 
                            sequence.AnchorMinStart, sequence.AnchorMinEnd, sequence.Duration, sequence.EaseFunction
                        ));
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.SizeDelta))
                        StartCoroutine(TaskSizeDelta(sequence.TargetComp.GetComponent<RectTransform>(), 
                            sequence.SizeDeltaStart, sequence.SizeDeltaEnd, sequence.Duration, sequence.EaseFunction
                        ));
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.Pivot))
                        StartCoroutine(TaskPivot(sequence.TargetComp.GetComponent<RectTransform>(), 
                            sequence.PivotStart, sequence.PivotEnd, sequence.Duration, sequence.EaseFunction
                        ));
                }
                else if(sequence.TargetType == Sequence.ObjectType.Transform)
                {
                    if(sequence.TargetTransTask.HasFlag(Sequence.TransTask.LocalPosition))
                        StartCoroutine(TaskLocalPosition(sequence.TargetComp.transform, 
                            sequence.LocalPositionStart, sequence.LocalPositionEnd, sequence.Duration, sequence.EaseFunction
                        ));
                    if(sequence.TargetTransTask.HasFlag(Sequence.TransTask.LocalScale))
                        StartCoroutine(TaskLocalEulerAngles(sequence.TargetComp.transform, 
                            sequence.LocalEulerAnglesStart, sequence.LocalEulerAnglesEnd, sequence.Duration, sequence.EaseFunction
                        ));
                    if(sequence.TargetTransTask.HasFlag(Sequence.TransTask.LocalEulerAngles))
                        StartCoroutine(TaskLocalScale(sequence.TargetComp.transform, 
                            sequence.LocalScaleStart, sequence.LocalScaleEnd, sequence.Duration, sequence.EaseFunction
                        ));
                }
                else if(sequence.TargetType == Sequence.ObjectType.Image)
                {
                    if(sequence.TargetImgTask.HasFlag(Sequence.ImgTask.Color))
                        StartCoroutine(TaskColor(sequence.TargetComp.GetComponent<Image>(), 
                            sequence.ColorStart, sequence.ColorEnd, sequence.Duration, sequence.EaseFunction
                        ));
                    if(sequence.TargetImgTask.HasFlag(Sequence.ImgTask.FillAmount))
                        StartCoroutine(TaskFillAmount(sequence.TargetComp.GetComponent<Image>(), 
                            sequence.FillAmountStart, sequence.FillAmountEnd, sequence.Duration, sequence.EaseFunction
                        ));
                }
                else if(sequence.TargetType == Sequence.ObjectType.CanvasGroup)
                {
                    if(sequence.TargetCgTask.HasFlag(Sequence.CgTask.Alpha))
                        StartCoroutine(TaskAlpha(sequence.TargetComp.GetComponent<CanvasGroup>(), 
                            sequence.AlphaStart, sequence.AlphaEnd, sequence.Duration, sequence.EaseFunction
                        ));
                }
                else if(sequence.TargetType == Sequence.ObjectType.Camera)
                {
                    if(sequence.TargetCamTask.HasFlag(Sequence.CamTask.BackgroundColor))
                        StartCoroutine(TaskBackgroundColor(sequence.TargetComp.GetComponent<Camera>(), 
                            sequence.BackgroundColorStart, sequence.BackgroundColorEnd, sequence.Duration, sequence.EaseFunction
                        ));
                    if(sequence.TargetCamTask.HasFlag(Sequence.CamTask.OrthographicSize))
                        StartCoroutine(TaskOrthographicSize(sequence.TargetComp.GetComponent<Camera>(), 
                            sequence.OrthographicSizeStart, sequence.OrthographicSizeEnd, sequence.Duration, sequence.EaseFunction
                        ));
                }
            }
            else if(sequence.SequenceType == Sequence.Type.Wait)
            {
                yield return new WaitForSecondsRealtime(sequence.Duration);
            }
            else if(sequence.SequenceType == Sequence.Type.SetActiveAllInput)
            {
                Singleton.Instance.Game.SetActiveAllInput(sequence.IsActivating);
            }
            else if(sequence.SequenceType == Sequence.Type.SetActive)
            {
                if(sequence.Target == null)
                {
                    // Debug.LogError("Please assign Target for Sequence at "+sequence.StartTime.ToString()+"s");
                    continue;
                }
                sequence.Target.SetActive(sequence.IsActivating);
            }
            else if(sequence.SequenceType == Sequence.Type.SFX)
            {
                if(sequence.SFX == null)
                {
                    // Debug.LogWarning("Please assign SFX for Sequence at "+sequence.StartTime.ToString()+"s");
                    continue;
                }
                Singleton.Instance.Audio.PlaySound(sequence.SFX);
            }
            else if(sequence.SequenceType == Sequence.Type.LoadScene)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sequence.SceneToLoad);
            }
            else if(sequence.SequenceType == Sequence.Type.UnityEvent)
            {
                sequence.Event?.Invoke();
            }
        }

        atEndEvents?.Invoke(); //Function to call at end

        atEndEvents = null;
        atTimeEvents.Clear();
        atTimes.Clear();
    }


    IEnumerator PlayReversedAnimation()
    {
        Singleton.LoadSingleton();
        Array.Reverse(AnimationSequence);
        yield return StartCoroutine(PlayAnimation());
        Array.Reverse(AnimationSequence);
    }

#region Tasks

#region RectTransform
    IEnumerator TaskAnchoredPosition(RectTransform rt, Vector3 start, Vector3 end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            rt.anchoredPosition = Vector3.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        rt.anchoredPosition = end;
    }
    IEnumerator TaskLocalScale(RectTransform rt, Vector3 start, Vector3 end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            rt.localScale = Vector3.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        rt.localScale = end;
    }
    IEnumerator TaskLocalEulerAngles(RectTransform rt, Vector3 start, Vector3 end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            rt.localEulerAngles = Vector3.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        rt.localEulerAngles = end;
    }
    IEnumerator TaskAnchorMax(RectTransform rt, Vector3 start, Vector3 end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            rt.anchorMax = Vector3.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        rt.anchorMax = end;
    }
    IEnumerator TaskAnchorMin(RectTransform rt, Vector3 start, Vector3 end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            rt.anchorMin = Vector3.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        rt.anchorMin = end;
    }
    IEnumerator TaskSizeDelta(RectTransform rt, Vector3 start, Vector3 end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            rt.sizeDelta = Vector3.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        rt.sizeDelta = end;
    }
    IEnumerator TaskPivot(RectTransform rt, Vector3 start, Vector3 end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            rt.pivot = Vector3.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        rt.pivot = end;
    }
#endregion RectTransform

#region TransformTask
    IEnumerator TaskLocalPosition(Transform trans, Vector3 start, Vector3 end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            trans.localPosition = Vector3.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        trans.localPosition = end;
    }
    IEnumerator TaskLocalScale(Transform trans, Vector3 start, Vector3 end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            trans.localScale = Vector3.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        trans.localScale = end;
    }
    IEnumerator TaskLocalEulerAngles(Transform trans, Vector3 start, Vector3 end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            trans.localEulerAngles = Vector3.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        trans.localEulerAngles = end;
    }
#endregion TransformTask

#region ImageTask
    IEnumerator TaskColor(Image img, Color start, Color end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            img.color = Color.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        img.color = end;
    }
    IEnumerator TaskFillAmount(Image img, float start, float end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            img.fillAmount = Mathf.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        img.fillAmount = end;
    }
#endregion ImageTask

#region CanvasGroupTask
    IEnumerator TaskAlpha(CanvasGroup cg, float start, float end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            cg.alpha = Mathf.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        cg.alpha = end;
    }
#endregion CanvasGroupTask

#region ImageTask
    IEnumerator TaskBackgroundColor(Camera cam, Color start, Color end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            cam.backgroundColor = Color.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        cam.backgroundColor = end;
    }
    IEnumerator TaskOrthographicSize(Camera cam, float start, float end, float duration, Ease.Function easeFunction)
    {
        float startTime = Time.time;
        float t = (Time.time-startTime)/duration;
        while (t <= 1)
        {
            t = Mathf.Clamp((Time.time-startTime)/duration, 0, 2);
            cam.orthographicSize = Mathf.LerpUnclamped(start, end, easeFunction(t));
            yield return null;
        }
        cam.orthographicSize = end;
    }
#endregion ImageTask
#endregion Tasks

#region Event
    public delegate void AnimationUIEvent();
    AnimationUIEvent atEndEvents;
    List<AnimationUIEvent> atTimeEvents = new List<AnimationUIEvent>();
    List<float> atTimes = new List<float>();

    IEnumerator AtTimeEvent(AnimationUIEvent atTimeEvent, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        atTimeEvent();
    }
    public AnimationUI AddFunctionAt(float time, AnimationUIEvent func)
    {
        atTimes.Add(time);
        atTimeEvents.Add(func);
        return this;
    }
    
    public AnimationUI AddFunctionAtEnd(AnimationUIEvent func)
    {
        atEndEvents += func;
        return this;
    }
#endregion Event




#if UNITY_EDITOR
    void ForceRepaint()
    {
        if (!Application.isPlaying)
        {
            UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
            UnityEditor.SceneView.RepaintAll();
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
            // Editor.up
        }
    }
    void OnDrawGizmos()
    {
        ForceRepaint();
    }
    void Update()
    {
        if(Application.isPlaying)return;
        ForceRepaint();

        if(IsPlayingInEditMode && CurrentTime < TotalDuration)
        {
            CurrentTime = Mathf.Clamp(Time.time - _startTime, 0, TotalDuration);
            UpdateSequence(CurrentTime);
        } 
        else
        {
            if(UpdateSequence != null && IsPlayingInEditMode)UpdateSequence(TotalDuration); //Make sure the latest frame is called
            IsPlayingInEditMode = false;
        }
    }
    public void UpdateBySlider()
    {
        if(Application.isPlaying)return;
        if(IsPlayingInEditMode)return;
        InitFunction();
        if(UpdateSequence != null)UpdateSequence(CurrentTime);
    }
    [HideInInspector] public float CurrentTime = 0; // Don't forget this variable might be in build
    [HideInInspector] public float TotalDuration = 0;
    [HideInInspector] public bool IsPlayingInEditMode = false;
    float _startTime = 0;
    public void PreviewAnimation()
    {
        InitFunction();
        if(UpdateSequence == null)
        {
            Debug.Log("No animation exist");
            return;
        }
        _startTime = Time.time;
        CurrentTime = 0;
        IsPlayingInEditMode = true;
        UpdateSequence(0);// Make sure the first frame is called
    }
    public void PreviewStart()
    {
        InitFunction();
        if(UpdateSequence == null)
        {
            Debug.Log("No animation exist");
            return;
        }
        CurrentTime = 0;
        IsPlayingInEditMode = false;
        UpdateSequence(0);
    }
    public void PreviewEnd()
    {
        CurrentTime = TotalDuration;
        InitFunction();
        if(UpdateSequence == null)
        {
            Debug.Log("No animation exist");
            return;
        }
        IsPlayingInEditMode = false;
        CurrentTime = TotalDuration;
        UpdateSequence(Mathf.Clamp01(TotalDuration));
    }
    void Reset() //For the default value. A hacky way because the inspector reset the value for Serialized class
    {
        AnimationSequence = new Sequence[1]
        {
            new Sequence()
        };
    }


    public delegate void Animation(float t);
    public Animation UpdateSequence;
#region timing
    public void InitTime()
    {
        TotalDuration = 0;
        foreach(Sequence sequence in AnimationSequence)
        {
            TotalDuration += (sequence.SequenceType == Sequence.Type.Wait) ? sequence.Duration : 0;
        }
        // for case when the duration of a non wait is bigger
        float currentTimeCheck = 0;
        float tempTotalDuration = TotalDuration;
        foreach(Sequence sequence in AnimationSequence)
        {
            currentTimeCheck += (sequence.SequenceType == Sequence.Type.Wait) ? sequence.Duration : 0;
            if(sequence.SequenceType == Sequence.Type.Animation)
            {
                if(TotalDuration < currentTimeCheck + sequence.Duration)
                {
                    TotalDuration = currentTimeCheck + sequence.Duration;
                }
            }
        }
        CurrentTime = Mathf.Clamp(CurrentTime, 0, TotalDuration);
    }
#endregion timing
    void InitFunction()//For preview
    {
        UpdateSequence = null;
        Singleton.LoadSingleton();
        foreach(Sequence sequence in AnimationSequence)
        {
            // sequence.IsDone = false;
            if(sequence.SequenceType == Sequence.Type.Animation)
            {
                if(sequence.TargetComp == null)
                {
                    Debug.Log("Please assign Target");
                    return;
                }
                sequence.Init();
                if(sequence.TargetType == Sequence.ObjectType.RectTransform)
                {
                    RectTransform rt = sequence.TargetComp.GetComponent<RectTransform>();
                    void RtAnchoredPosition(float t)
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                rt.anchoredPosition = sequence.AnchoredPositionEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                rt.anchoredPosition
                                = Vector3.LerpUnclamped(sequence.AnchoredPositionStart, sequence.AnchoredPositionEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            rt.anchoredPosition = sequence.AnchoredPositionStart;
                        }
                        else
                        {
                            rt.anchoredPosition
                                = Vector3.LerpUnclamped(sequence.AnchoredPositionStart, sequence.AnchoredPositionEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    void RtLocalEulerAngles(float t)
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                rt.localEulerAngles = sequence.LocalEulerAnglesEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                rt.localEulerAngles
                                = Vector3.LerpUnclamped(sequence.LocalEulerAnglesStart, sequence.LocalEulerAnglesEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            rt.localEulerAngles = sequence.LocalEulerAnglesStart;
                        }
                        else
                        {
                            rt.localEulerAngles
                                = Vector3.LerpUnclamped(sequence.LocalEulerAnglesStart, sequence.LocalEulerAnglesEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    void RtLocalScale(float t) 
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                rt.localScale = sequence.LocalScaleEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                rt.localScale
                                = Vector3.LerpUnclamped(sequence.LocalScaleStart, sequence.LocalScaleEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            rt.localScale = sequence.LocalScaleStart;
                        }
                        else
                        {
                            rt.localScale
                                = Vector3.LerpUnclamped(sequence.LocalScaleStart, sequence.LocalScaleEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    void RtSizeDelta(float t) 
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                rt.sizeDelta = sequence.SizeDeltaEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                rt.sizeDelta
                                = Vector3.LerpUnclamped(sequence.SizeDeltaStart, sequence.SizeDeltaEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            rt.sizeDelta = sequence.SizeDeltaStart;
                        }
                        else
                        {
                            rt.sizeDelta
                                = Vector3.LerpUnclamped(sequence.SizeDeltaStart, sequence.SizeDeltaEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }    
                    void AnchorMin(float t)
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                rt.anchorMin = sequence.AnchorMinEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                rt.anchorMin
                                = Vector3.LerpUnclamped(sequence.AnchorMinStart, sequence.AnchorMinEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            rt.anchorMin = sequence.AnchorMinStart;
                        }
                        else
                        {
                            rt.anchorMin
                                = Vector3.LerpUnclamped(sequence.AnchorMinStart, sequence.AnchorMinEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    void AnchorMax(float t) 
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                rt.anchorMax = sequence.AnchorMaxEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                rt.anchorMax
                                = Vector3.LerpUnclamped(sequence.AnchorMaxStart, sequence.AnchorMaxEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            rt.anchorMax = sequence.AnchorMaxStart;
                        }
                        else
                        {
                            rt.anchorMax
                                = Vector3.LerpUnclamped(sequence.AnchorMaxStart, sequence.AnchorMaxEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    void Pivot(float t) 
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                rt.pivot = sequence.PivotEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                rt.pivot
                                = Vector3.LerpUnclamped(sequence.PivotStart, sequence.PivotEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            rt.pivot = sequence.PivotStart;
                        }
                        else
                        {
                            rt.pivot
                                = Vector3.LerpUnclamped(sequence.PivotStart, sequence.PivotEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    
                    
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.AnchoredPosition))
                        UpdateSequence += RtAnchoredPosition;
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.LocalEulerAngles))
                        UpdateSequence += RtLocalEulerAngles;
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.LocalScale))
                        UpdateSequence += RtLocalScale;
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.SizeDelta))
                        UpdateSequence += RtSizeDelta;
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.AnchorMax))
                        UpdateSequence += AnchorMax;
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.AnchorMin))
                        UpdateSequence += AnchorMin;
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.Pivot))
                        UpdateSequence += Pivot;
                    
                }
                else if(sequence.TargetType == Sequence.ObjectType.Transform)
                {
                    Transform trans = sequence.TargetComp.transform;
                    void TransLocalPosition(float t) 
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                trans.localPosition = sequence.LocalPositionEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                trans.localPosition
                                = Vector3.LerpUnclamped(sequence.LocalPositionStart, sequence.LocalPositionEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            trans.localPosition = sequence.LocalPositionStart;
                        }
                        else
                        {
                            trans.localPosition
                                = Vector3.LerpUnclamped(sequence.LocalPositionStart, sequence.LocalPositionEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    void TransLocalEulerAngles(float t)
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                trans.localEulerAngles = sequence.LocalEulerAnglesEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                trans.localEulerAngles
                                = Vector3.LerpUnclamped(sequence.LocalEulerAnglesStart, sequence.LocalEulerAnglesEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            trans.localEulerAngles = sequence.LocalEulerAnglesStart;
                        }
                        else
                        {
                            trans.localEulerAngles
                                = Vector3.LerpUnclamped(sequence.LocalEulerAnglesStart, sequence.LocalEulerAnglesEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    void TransLocalScale(float t) 
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                trans.localScale = sequence.LocalScaleEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                trans.localScale
                                = Vector3.LerpUnclamped(sequence.LocalScaleStart, sequence.LocalScaleEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            trans.localScale = sequence.LocalScaleStart;
                        }
                        else
                        {
                            trans.localScale
                                = Vector3.LerpUnclamped(sequence.LocalScaleStart, sequence.LocalScaleEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    
                    if(sequence.TargetTransTask.HasFlag(Sequence.TransTask.LocalPosition))
                        UpdateSequence += TransLocalPosition;
                    if(sequence.TargetTransTask.HasFlag(Sequence.TransTask.LocalEulerAngles))
                        UpdateSequence += TransLocalEulerAngles;
                    if(sequence.TargetTransTask.HasFlag(Sequence.TransTask.LocalScale))
                        UpdateSequence += TransLocalScale;
                }
                else if(sequence.TargetType == Sequence.ObjectType.Image)
                {
                    Image img = sequence.TargetComp.GetComponent<Image>();
                    void ImgColor(float t) 
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                img.color = sequence.ColorEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                img.color
                                = Color.LerpUnclamped(sequence.ColorStart, sequence.ColorEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            img.color = sequence.ColorStart;
                        }
                        else
                        {
                            img.color
                                = Color.LerpUnclamped(sequence.ColorStart, sequence.ColorEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    void TransFillAmount(float t) 
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                img.fillAmount = sequence.FillAmountEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                img.fillAmount
                                = Mathf.LerpUnclamped(sequence.FillAmountStart, sequence.FillAmountEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            img.fillAmount = sequence.FillAmountStart;
                        }
                        else
                        {
                            img.fillAmount
                            = Mathf.LerpUnclamped(sequence.FillAmountStart, sequence.FillAmountEnd,
                                sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    
                    if(sequence.TargetImgTask.HasFlag(Sequence.ImgTask.Color))
                        UpdateSequence += ImgColor;
                    if(sequence.TargetImgTask.HasFlag(Sequence.ImgTask.FillAmount))
                        UpdateSequence += TransFillAmount;
                }
                else if(sequence.TargetType == Sequence.ObjectType.CanvasGroup)
                {
                    CanvasGroup cg = sequence.TargetComp.GetComponent<CanvasGroup>();
                    void CgAlpha(float t) 
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                cg.alpha = sequence.AlphaEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                cg.alpha
                                = Mathf.LerpUnclamped(sequence.AlphaStart, sequence.AlphaEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            cg.alpha = sequence.AlphaStart;
                        }
                        else
                        {
                            cg.alpha
                            = Mathf.LerpUnclamped(sequence.AlphaStart, sequence.AlphaEnd,
                                sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    
                    if(sequence.TargetCgTask.HasFlag(Sequence.CgTask.Alpha))
                        UpdateSequence += CgAlpha;
                }
                else if(sequence.TargetType == Sequence.ObjectType.Camera)
                {
                    Camera cam = sequence.TargetComp.GetComponent<Camera>();
                    void CamBackgroundColor(float t) 
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                cam.backgroundColor = sequence.BackgroundColorEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                cam.backgroundColor
                                = Color.LerpUnclamped(sequence.BackgroundColorStart, sequence.BackgroundColorEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            cam.backgroundColor = sequence.BackgroundColorStart;
                        }
                        else
                        {
                            cam.backgroundColor
                            = Color.LerpUnclamped(sequence.BackgroundColorStart, sequence.BackgroundColorEnd,
                                sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    void CamOrthographicSize(float t) 
                    {
                        if(!sequence.IsDone)
                        {
                            if(t-sequence.StartTime < 0)return;
                            if(t-sequence.StartTime >= sequence.Duration)
                            {
                                sequence.IsDone = true;
                                cam.orthographicSize = sequence.OrthographicSizeEnd;
                            }
                            else  // 0 < t-sequence.StartTime < sequence.Duration
                            {
                                cam.orthographicSize
                                = Mathf.LerpUnclamped(sequence.OrthographicSizeStart, sequence.OrthographicSizeEnd,
                                    sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                            }
                        }
                        else if(t-sequence.StartTime < 0) // IsDone == true && t-sequence.StartTime < 0 
                        {
                            sequence.IsDone = false;
                            cam.orthographicSize = sequence.OrthographicSizeStart;
                        }
                        else
                        {
                            cam.orthographicSize
                            = Mathf.LerpUnclamped(sequence.OrthographicSizeStart, sequence.OrthographicSizeEnd,
                                sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                        }
                    }
                    
                    if(sequence.TargetCamTask.HasFlag(Sequence.CamTask.BackgroundColor))
                        UpdateSequence += CamBackgroundColor;
                    if(sequence.TargetCamTask.HasFlag(Sequence.CamTask.OrthographicSize))
                        UpdateSequence += CamOrthographicSize;
                }
                else if(sequence.TargetType == Sequence.ObjectType.UnityEventDynamic)
                {
                    Image img = sequence.TargetComp.GetComponent<Image>();
                    void EventDynamic(float t) 
                    {
                        if(t-sequence.StartTime < 0)return;
                        sequence.EventDynamic?.Invoke(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration));
                    }
                    UpdateSequence += EventDynamic;
                }
                
            }
            else if(sequence.SequenceType == Sequence.Type.Wait)
            {

            }
            else if(sequence.SequenceType == Sequence.Type.SetActiveAllInput)
            {
                void SetActiveALlInput(float t)
                {
                    float time = Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration);
                    if(!sequence.IsDone) // so that SetActiveAllInput in the first frame can also be called
                    {
                        if(t - sequence.StartTime > -0.01f)
                        {
                            sequence.IsDone = true;
                            Singleton.Instance.Game.SetActiveAllInput(sequence.IsActivating);
                        }
                    }
                    else if(t - sequence.StartTime < 0)
                    {
                        sequence.IsDone = false;
                        Singleton.Instance.Game.SetActiveAllInput(!sequence.IsActivating);
                    }
                }
                sequence.IsDone = false;
                UpdateSequence += SetActiveALlInput;
            }
            else if(sequence.SequenceType == Sequence.Type.SetActive)
            {
                void SetActive(float t)
                {
                    float time = Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration);
                    if(!sequence.IsDone) // so that SetActiveAllInput in the first frame can also be called
                    {
                        if(t - sequence.StartTime > -0.01f)
                        {
                            sequence.IsDone = true;
                            if(sequence.Target == null)
                            {
                                Debug.Log("Please assign Target for Sequence at "+sequence.StartTime.ToString()+"s");
                                return;
                            }
                            sequence.Target.SetActive(sequence.IsActivating);
                        }
                    }
                    else if(t - sequence.StartTime < 0)
                    {
                        sequence.IsDone = false;
                        if(sequence.Target == null)
                        {
                            Debug.Log("Please assign Target for Sequence at "+sequence.StartTime.ToString()+"s");
                            return;
                        }
                        sequence.Target.SetActive(!sequence.IsActivating);
                    }
                }
                sequence.IsDone = false;
                UpdateSequence += SetActive;
            }
            else if(sequence.SequenceType == Sequence.Type.SFX)
            {
                void SFX(float t)
                {
                    float time = Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration);
                    if(!sequence.IsDone) // so that SetActiveAllInput in the first frame can also be called
                    {
                        if(t - sequence.StartTime > -0.01f)
                        {
                            sequence.IsDone = true;
                            if(sequence.SFX == null)
                            {
                                Debug.Log("Please assign SFX for Sequence at "+sequence.StartTime.ToString()+"s");
                                return;
                            }
                            Singleton.Instance.Audio.PlaySound(sequence.SFX);
                        }
                    }
                    else if(t - sequence.StartTime < 0)
                    {
                        if(sequence.SFX == null)
                        {
                            Debug.Log("Please assign  for Sequence at "+sequence.StartTime.ToString()+"s");
                            return;
                        }
                        sequence.IsDone = false;
                        Singleton.Instance.Audio.PlaySound(sequence.SFX);
                    }
                }
                sequence.IsDone = false;
                UpdateSequence += SFX;
            }
            else if(sequence.SequenceType == Sequence.Type.UnityEvent)
            {
                void UnityEvent(float t)
                {
                    float time = Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration);
                    if(!sequence.IsDone) 
                    {
                        // -0.01f so that SetActiveAllInput in the first frame can also be called
                        if(t - sequence.StartTime > -0.01f) //Nested conditional may actually more performant in this case
                        {
                            sequence.IsDone = true;
                            sequence.Event?.Invoke();
                        } 
                    }
                    else if(t - sequence.StartTime < 0)
                    {
                        sequence.IsDone = false;
                        sequence.Event?.Invoke();
                    } 
                }
                sequence.IsDone = false;
                UpdateSequence += UnityEvent;
            }
        }
    }
#endif
}
