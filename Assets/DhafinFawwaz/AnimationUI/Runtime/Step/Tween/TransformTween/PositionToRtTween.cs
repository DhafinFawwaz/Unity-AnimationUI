using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class PositionToRtTween : TransformTween<Transform, Vector3, RectTransform, Vector3>
    {
        protected override Func<Vector3, RectTransform, float, Vector3> InterpolationFunction => (from, to, t) => Vector3.LerpUnclamped(from, RectTransformToWorldPosition(to, _mainCamCached), t);
        public override void ApplyInterpolation(Vector3 value) => Target.position = value;
        public override void SetFromAsTargetValue() => From = Target.position;
        public override void SetToAsTargetValue() => To.position = Target.position;

        Camera _cam;

        Camera _mainCamCached {
            get {
                if(_cam == null) {
                    _cam = Camera.main;
                }
                return _cam;
            }
        }

        Vector3 RectTransformToWorldPosition(RectTransform rectTransform, Camera worldCamera) {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, rectTransform.position);
            Vector3 worldPos = worldCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, worldCamera.nearClipPlane));
            return worldPos;
        }

    }
}
