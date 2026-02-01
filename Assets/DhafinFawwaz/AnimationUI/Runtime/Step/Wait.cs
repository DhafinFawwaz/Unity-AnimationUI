using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable] 
    [BGColor("#0000ff15")]
    public class Wait : Step, IWaitable
    {
        public float Duration = 0.5f;
        public float GetDuration(){
            return Duration;
        }
        public override string GetDisplayName(){
            return $"[{Duration}s]";
        }
    }
}
