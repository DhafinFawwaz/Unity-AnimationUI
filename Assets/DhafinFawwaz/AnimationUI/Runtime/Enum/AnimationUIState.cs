namespace DhafinFawwaz.AnimationUI {
    /// <summary>
    /// Represents the current playback state of an AnimationUI component.
    /// </summary>
    public enum AnimationUIState {
        /// <summary>
        /// The animation is stopped or has never been played.
        /// </summary>
        IsNotPlaying,
        
        /// <summary>
        /// The animation is currently playing.
        /// </summary>
        IsPlaying,
        
        /// <summary>
        /// The animation is paused and can be resumed.
        /// </summary>
        IsPaused
    }
}