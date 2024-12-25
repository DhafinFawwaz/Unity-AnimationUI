using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {

    // interface for component that will have a target with value changed in one shot and can also be reverted
    public interface IExecutable
    {
        public void Execute();

        public void Dexecute();

    }
}
