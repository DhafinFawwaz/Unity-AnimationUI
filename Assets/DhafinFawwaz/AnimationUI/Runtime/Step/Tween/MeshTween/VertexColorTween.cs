using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class VertexColorTween : MeshTween<MeshFilter, Color32, Color32, Color32>
    {
        protected override Func<Color32, Color32, float, Color32> InterpolationFunction => Color32.LerpUnclamped;
        public override void ApplyInterpolation(Color32 value) {
            if(!Application.isPlaying) return;
            Color32[] colors = Target.mesh.colors32;
            if (colors.Length == 0) colors = new Color32[Target.mesh.vertexCount];
            for (int i = 0; i < colors.Length; i++) {
                colors[i] = value;
            }
            Target.mesh.colors32 = colors;
        }
        public override void SetFromAsTargetValue() => From = Target.mesh.colors[0];
        public override void SetToAsTargetValue() => To = Target.mesh.colors[0];

    }
}
