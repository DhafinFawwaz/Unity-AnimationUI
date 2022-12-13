using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class EnumTest : MonoBehaviour
{
    [System.Serializable]
    public class RtTask
    {
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
    }


    public RtTask TargetRtTask;

}
