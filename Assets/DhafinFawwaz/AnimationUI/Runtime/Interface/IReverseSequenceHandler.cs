using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {
    // every compoenent that implement this interface will have a method that will be called when the "Reverse Sequence" button is clicked
    public interface IReverseSequenceHandler
    {
        public void OnSequenceReversed();
    }
}
