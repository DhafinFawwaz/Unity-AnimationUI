using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
[System.Serializable]
public class Sequence
{
    public string AtTime;
    public float StartTime;
    public enum Type{
        Animation, Wait, SetActiveAllInput, SetActive, SFX, UnityEvent
    }
    public Type SequenceType = Type.Animation;
    public Ease.Type EaseType = Ease.Type.Out;
    public Ease.Power EasePower = Ease.Power.Quart;
    public enum ObjectType{// Only for Animation
        Automatic, RectTransform, Transform, Image, UnityEvent
    }
    public ObjectType TargetType = ObjectType.Automatic;
    
    public Component TargetComp;

    public float Duration;

#region SetActiveALlInput
    public bool IsButtonBlocking;
#endregion SetActiveALlInput


#region SetActive
    public GameObject Target;
    public bool IsActivating;
#endregion SetActive

#region SFX
    public AudioClip SFX;
#endregion SFX

#region UnityEvent
    public UnityEvent Event;
    public bool IsUnfolded = true;
#endregion UnityEvent


#region RectTransform
    // [System.Serializable]
    // public class RtTask
    // {
    //     [System.Flags]
    //     public enum RtType
    //     {
    //         None = 0,
    //         AnchoredPosition = 1 << 0,
    //         LocalScale = 1 << 1,
    //         LocalRotation = 1 << 2,
    //     }

    //     public RtType TargetRtType;

    //     public Vector3 AnchoredPositionStart;
    //     public Vector3 AnchoredPositionEnd;

    //     public Vector3 LocalScaleStart;
    //     public Vector3 LocalScaleEnd;

    //     public Vector3 LocalRotationStart;
    //     public Vector3 LocalRotationEnd;

    // }
    // public RtTask TargetRtTask;
    // [System.Serializable]
    // public class RtTask
    // {
        [System.Flags]
        public enum RtType
        {
            None = 0,
            AnchoredPosition = 1 << 0,
            LocalScale = 1 << 1,
            LocalRotation = 1 << 2,
            // HasCompletedTheGame = 1 << 3,
            // HasAllAchievments = 1 << 4,
        }

        public RtType TargetRtType;

        public Vector3 AnchoredPositionStart;
        public Vector3 AnchoredPositionEnd;
    // }
    // public RtTask TargetRtTask;
#endregion RectTransform



#region Transform
#endregion Transform

#region Image
#endregion Image

}
