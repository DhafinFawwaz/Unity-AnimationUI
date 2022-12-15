using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
[ExecuteAlways]
#endif
public class AnimationUI : MonoBehaviour
{
    public Sequence[] AnimationSequence;
    public void Play() => StartCoroutine(PlayAnimation());
    public void PlayReversed() => StartCoroutine(PlayAnimationReversed());
    IEnumerator PlayAnimation()
    {
        foreach(Sequence sequence in AnimationSequence)
        {
            switch(sequence.SequenceType)
            {
                case Sequence.Type.Animation:
                    // StartCoroutine(EaseOutRt(sequence.TargetRt, sequence.StartPosition, sequence.EndPosition, sequence.Duration));
                    break;
                
                case Sequence.Type.Wait:
                    yield return new WaitForSecondsRealtime(sequence.Duration);
                    break;

                case Sequence.Type.SetActiveAllInput:
                    Singleton.Instance.Game.SetActiveAllInput(sequence.IsActivating);
                    break;

                case Sequence.Type.SetActive:
                    sequence.Target.SetActive(sequence.IsActivating);
                    break;

                case Sequence.Type.SFX:
                    Singleton.Instance.Audio.PlaySound(sequence.SFX);
                    break;
            }
        }
    }

    IEnumerator EaseOutRt(RectTransform Rt, Vector2 startPosition, Vector2 targetPosition, float duration)
    {
        float t = 0;
        while (t <= 1)
        {
            t += Time.unscaledDeltaTime/duration;
            Rt.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, Ease.InOutQuart(t));
            yield return null;
        }
        Rt.anchoredPosition = targetPosition;
    }



    IEnumerator PlayAnimationReversed()
    {
        yield return null;
    }

#if UNITY_EDITOR
    
    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            // UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
            // UnityEditor.SceneView.RepaintAll();
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }
    }
    void Update()
    {
        if(IsPlaying && CurrentTime < TotalDuration)
        {
            CurrentTime = Mathf.Clamp(Time.time - _startTime, 0, TotalDuration);
            foreach(Sequence sequence in AnimationSequence)
            {
                if(sequence.Duration == 0)UpdateSequence(1);
                else UpdateSequence(CurrentTime);
            }
        } else IsPlaying = false;
    }
    public void UpdateBySlider()
    {
        if(IsPlaying)return;
        if(UpdateSequence == null)
        {
            InitFunction();
            if(UpdateSequence == null)return;
        }
        foreach(Sequence sequence in AnimationSequence)
        {
            if(sequence.Duration == 0)UpdateSequence(1);
            else UpdateSequence?.Invoke(CurrentTime);
        }
    }
    [HideInInspector] public float CurrentTime = 0; // Don't forget this variable might be in build
    [HideInInspector] public float TotalDuration = 0;
    [HideInInspector] public bool IsPlaying = false;
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
        IsPlaying = true;
    }
    public void PreviewStart()
    {
        CurrentTime = 0;
        InitFunction();
        if(UpdateSequence == null)
        {
            Debug.Log("No animation exist");
            return;
        }
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
        CurrentTime = TotalDuration;
        foreach(Sequence sequence in AnimationSequence)
        {
            if(sequence.Duration == 0)UpdateSequence(1);
            else UpdateSequence(Mathf.Clamp01(CurrentTime));
        }
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
    void InitFunction()
    {
        UpdateSequence = null;
        foreach(Sequence sequence in AnimationSequence)
        {
            if(sequence.SequenceType == Sequence.Type.Animation)
            {
                sequence.Init();
                if(sequence.TargetType == Sequence.ObjectType.RectTransform)
                {
                    RectTransform rt = sequence.TargetComp.GetComponent<RectTransform>();
                    void RtAnchoredPosition(float t) 
                        => rt.anchoredPosition
                        = Vector3.LerpUnclamped(sequence.AnchoredPositionStart, sequence.AnchoredPositionEnd,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    void RtLocalEulerAngles(float t) 
                        => rt.localEulerAngles
                        = Vector3.LerpUnclamped(sequence.LocalEulerAnglesStart, sequence.LocalEulerAnglesStart,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    void RtLocalScale(float t) 
                        => rt.localScale
                        = Vector3.LerpUnclamped(sequence.LocalScaleStart, sequence.LocalScaleEnd,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    void RtSizeDelta(float t) 
                        => rt.sizeDelta
                        = Vector3.LerpUnclamped(sequence.SizeDeltaStart, sequence.SizeDeltaEnd,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    void AnchorMin(float t) 
                        => rt.sizeDelta
                        = Vector3.LerpUnclamped(sequence.AnchorMinStart, sequence.AnchorMinEnd,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    void AnchorMax(float t) 
                        => rt.sizeDelta
                        = Vector3.LerpUnclamped(sequence.AnchorMaxStart, sequence.AnchorMaxStart,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    void Pivot(float t) 
                        => rt.sizeDelta
                        = Vector3.LerpUnclamped(sequence.PivotStart, sequence.PivotStart,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    
                    
                    if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.AnchoredPosition))
                        UpdateSequence += RtAnchoredPosition;
                    else if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.LocalEulerAngles))
                        UpdateSequence += RtLocalEulerAngles;
                    else if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.LocalScale))
                        UpdateSequence += RtLocalScale;
                    else if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.SizeDelta))
                        UpdateSequence += RtSizeDelta;
                    else if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.AnchorMax))
                        UpdateSequence += AnchorMax;
                    else if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.AnchorMin))
                        UpdateSequence += AnchorMin;
                    else if(sequence.TargetRtTask.HasFlag(Sequence.RtTask.Pivot))
                        UpdateSequence += Pivot;
                    
                }
                else if(sequence.TargetType == Sequence.ObjectType.Transform)
                {
                    Transform trans = sequence.TargetComp.transform;
                    void TransLocalPosition(float t) 
                        => trans.localPosition
                        = Vector3.LerpUnclamped(sequence.LocalPositionStart, sequence.LocalPositionEnd,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    void TransLocalEulerAngles(float t) 
                        => trans.localEulerAngles
                        = Vector3.LerpUnclamped(sequence.LocalEulerAnglesStart, sequence.LocalEulerAnglesStart,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    void TransLocalScale(float t) 
                        => trans.localScale
                        = Vector3.LerpUnclamped(sequence.LocalScaleStart, sequence.LocalScaleEnd,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    
                    if(sequence.TargetTransTask.HasFlag(Sequence.TransTask.LocalPosition))
                        UpdateSequence += TransLocalPosition;
                    else if(sequence.TargetTransTask.HasFlag(Sequence.TransTask.LocalEulerAngles))
                        UpdateSequence += TransLocalEulerAngles;
                    else if(sequence.TargetTransTask.HasFlag(Sequence.TransTask.LocalScale))
                        UpdateSequence += TransLocalScale;
                }
                else if(sequence.TargetType == Sequence.ObjectType.Image)
                {
                    Image img = sequence.TargetComp.GetComponent<Image>();
                    void TransColor(float t) 
                        => img.color
                        = Color.LerpUnclamped(sequence.ColorStart, sequence.ColorEnd,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    void TransFillAmount(float t) 
                        => img.fillAmount
                        = Mathf.LerpUnclamped(sequence.FillAmountStart, sequence.FillAmountEnd,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    
                    if(sequence.TargetImgTask.HasFlag(Sequence.ImgTask.Color))
                        UpdateSequence += TransColor;
                    else if(sequence.TargetImgTask.HasFlag(Sequence.ImgTask.FillAmount))
                        UpdateSequence += TransFillAmount;
                }
                else if(sequence.TargetType == Sequence.ObjectType.UnityEventDynamic)
                {
                    Image img = sequence.TargetComp.GetComponent<Image>();
                    void TransColor(float t) 
                        => img.color
                        = Color.LerpUnclamped(sequence.ColorStart, sequence.ColorEnd,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    void TransFillAmount(float t) 
                        => img.fillAmount
                        = Mathf.LerpUnclamped(sequence.FillAmountStart, sequence.FillAmountEnd,
                            sequence.EaseFunction(Mathf.Clamp01((t-sequence.StartTime)/sequence.Duration)));
                    
                    if(sequence.TargetImgTask.HasFlag(Sequence.ImgTask.Color))
                        UpdateSequence += TransColor;
                    else if(sequence.TargetImgTask.HasFlag(Sequence.ImgTask.FillAmount))
                        UpdateSequence += TransFillAmount;
                }
            }
            else if(sequence.SequenceType == Sequence.Type.Wait)
            {

            }
            else if(sequence.SequenceType == Sequence.Type.SetActiveAllInput)
            {

            }
            else if(sequence.SequenceType == Sequence.Type.SetActive)
            {

            }
            else if(sequence.SequenceType == Sequence.Type.SFX)
            {

            }
            else if(sequence.SequenceType == Sequence.Type.UnityEvent)
            {

            }
        }
    }
#endif
}
