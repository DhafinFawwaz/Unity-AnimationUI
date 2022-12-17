using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
[System.Serializable]
public class Sequence
{
    public string AtTime;
    public float StartTime;
    
    public bool TriggerStart = false; //This automatically change to false immedietely after becoming true
    public bool TriggerEnd = false; //This automatically change to false immedietely after becoming true
    
    public float PropertyRectHeight;
    public float PropertyRectY;
    public enum Type{
        Animation, Wait, SetActiveAllInput, SetActive, SFX, LoadScene, UnityEvent
    }
    public Type SequenceType = Type.Animation;
    public Ease.Type EaseType = Ease.Type.Out;
    public Ease.Power EasePower = Ease.Power.Quart;
    public enum ObjectType{// Only for Animation
        Automatic, RectTransform, Transform, Image, CanvasGroup, Camera, UnityEventDynamic
    }
    public ObjectType TargetType = ObjectType.Automatic;
    
    public Component TargetComp;

    public float Duration = 0.5f;
    public UnityEvent<float> EventDynamic;
    public bool IsUnfolded = true;
    public bool IsDone = false;

#region SetActiveALlInput
    // public bool IsActivating = true;
#endregion SetActiveALlInput


#region SetActive
    public GameObject Target;
    public bool IsActivating = true;
#endregion SetActive

#region SFX
    public AudioClip SFX;
#endregion SFX

#region LoadScene
    public string SceneToLoad = "";
#endregion LoadScene

#region UnityEvent
    public UnityEvent Event;
#endregion UnityEvent


#region RectTransform
    [System.Flags]
    public enum RtTask
    {
        None = 0,
        AnchoredPosition = 1 << 0,
        LocalScale = 1 << 1,
        LocalEulerAngles = 1 << 2,
        SizeDelta = 1 << 3,
        AnchorMin = 1 << 4,
        AnchorMax = 1 << 5,
        Pivot = 1 << 6
    }

    public RtTask TargetRtTask = RtTask.AnchoredPosition;

    public Vector3 AnchoredPositionStart;
    public Vector3 AnchoredPositionEnd;

    public Vector3 LocalScaleStart;
    public Vector3 LocalScaleEnd;

    public Vector3 LocalEulerAnglesStart;
    public Vector3 LocalEulerAnglesEnd;

    public Vector3 SizeDeltaStart;
    public Vector3 SizeDeltaEnd;

    public Vector3 AnchorMinStart;
    public Vector3 AnchorMinEnd;

    public Vector3 AnchorMaxStart;
    public Vector3 AnchorMaxEnd;

    public Vector3 PivotStart;
    public Vector3 PivotEnd;
#endregion RectTransform

#region Transform
    [System.Flags]
    public enum TransTask
    {
        None = 0,
        LocalPosition = 1 << 0,
        LocalScale = 1 << 1,
        LocalEulerAngles = 1 << 2,
    }

    public TransTask TargetTransTask = TransTask.LocalPosition;

    public Vector3 LocalPositionStart;
    public Vector3 LocalPositionEnd;

    // public Vector3 LocalScaleStart;
    // public Vector3 LocalScaleEnd;

    // public Vector3 LocalEulerAnglesStart;
    // public Vector3 LocalEulerAnglesEnd;

#endregion Transform

#region Image
    [System.Flags]
    public enum ImgTask
    {
        None = 0,
        Color = 1 << 0,
        FillAmount = 1 << 1,
    }

    public ImgTask TargetImgTask = ImgTask.Color;

    public Color ColorStart;
    public Color ColorEnd;

    [Range(0, 1)] public float FillAmountStart;
    [Range(0, 1)] public float FillAmountEnd;
#endregion Image

#region CanvasGroup
    [System.Flags]
    public enum CgTask
    {
        None = 0,
        Alpha = 1 << 0,
    }
    public CgTask TargetCgTask = CgTask.Alpha;
    [Range(0, 1)] public float AlphaStart;
    [Range(0, 1)] public float AlphaEnd;
#endregion CanvasGroup

#region Camera
    [System.Flags]
    public enum CamTask
    {
        None = 0,
        BackgroundColor = 1 << 0,
        OrthographicSize = 2 << 0,
    }
    public CamTask TargetCamTask = CamTask.BackgroundColor;
    public Color BackgroundColorStart;
    public Color BackgroundColorEnd;
    public float OrthographicSizeStart;
    public float OrthographicSizeEnd;
#endregion Camera

    public Ease.Function EaseFunction;
    public void Init()
    {
        EaseFunction = Ease.GetEase(EaseType, EasePower);
    }


    public int TestInt = 0;

}
