using UnityEngine;
using UnityEngine.Events;

namespace DhafinFawwaz.AnimationUILib
{

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
        Automatic, RectTransform, Transform, Image, CanvasGroup, Camera, TextMeshPro, UnityEventDynamic
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
    public enum SFXMethod{
        File, Index
    }
    public SFXMethod PlaySFXBy = SFXMethod.File;
    public AudioClip SFXFile;
    public int SFXIndex;
#endregion SFX

#region LoadScene
    public string SceneToLoad = "";
#endregion LoadScene

#region UnityEvent
    public UnityEvent Event;
#endregion UnityEvent

    public enum State{ // For snapping
        Before, During, After
    }

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

    public State AnchoredPositionState = State.Before;
    public Vector3 AnchoredPositionStart;
    public Vector3 AnchoredPositionEnd;

    public State LocalScaleState = State.Before;
    public Vector3 LocalScaleStart;
    public Vector3 LocalScaleEnd;

    public State LocalEulerAnglesState = State.Before;
    public Vector3 LocalEulerAnglesStart;
    public Vector3 LocalEulerAnglesEnd;

    public State SizeDeltaState = State.Before;
    public Vector3 SizeDeltaStart;
    public Vector3 SizeDeltaEnd;

    public State AnchorMinState = State.Before;
    public Vector3 AnchorMinStart;
    public Vector3 AnchorMinEnd;

    public State AnchorMaxState = State.Before;
    public Vector3 AnchorMaxStart;
    public Vector3 AnchorMaxEnd;

    public State PivotState = State.Before;
    public Vector3 PivotStart;
    public Vector3 PivotEnd;
#endregion RectTransform

#region Transform
    public State TransState = State.Before;
    [System.Flags]
    public enum TransTask
    {
        None = 0,
        LocalPosition = 1 << 0,
        LocalScale = 1 << 1,
        LocalEulerAngles = 1 << 2,
    }

    public TransTask TargetTransTask = TransTask.LocalPosition;

    public State LocalPositionState = State.Before;
    public Vector3 LocalPositionStart;
    public Vector3 LocalPositionEnd;

    // public Vector3 LocalScaleStart;
    // public Vector3 LocalScaleEnd;

    // public Vector3 LocalEulerAnglesStart;
    // public Vector3 LocalEulerAnglesEnd;

#endregion Transform

#region Image
    public State ImgState = State.Before;
    [System.Flags]
    public enum ImgTask
    {
        None = 0,
        Color = 1 << 0,
        FillAmount = 1 << 1,
    }

    public ImgTask TargetImgTask = ImgTask.Color;

    public State ColorState = State.Before;
    public Color ColorStart;
    public Color ColorEnd;

    public State FillAmountState = State.Before;
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

    public State AlphaState = State.Before;
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

    public State BackgroundColorState = State.Before;
    public Color BackgroundColorStart;
    public Color BackgroundColorEnd;


    public State OrthographicSizeState = State.Before;
    public float OrthographicSizeStart;
    public float OrthographicSizeEnd;
#endregion Camera

#region TextMeshPro
    [System.Flags]
    public enum TextMeshProTask
    {
        None = 0,
        Color = 1 << 0,
        MaxVisibleCharacters = 2 << 0,
    }
    public TextMeshProTask TargetTextMeshProTask = TextMeshProTask.Color;

    public State TextMeshProColorState = State.Before;
    public Color TextMeshProColorStart;
    public Color TextMeshProColorEnd;


    public State MaxVisibleCharactersState = State.Before;
    public int MaxVisibleCharactersStart;
    public int MaxVisibleCharactersEnd;
#endregion Camera    

    public Ease.Function EaseFunction = Ease.OutQuart;
    public void Init()
    {
        EaseFunction = Ease.GetEase(EaseType, EasePower);
    }
}

}