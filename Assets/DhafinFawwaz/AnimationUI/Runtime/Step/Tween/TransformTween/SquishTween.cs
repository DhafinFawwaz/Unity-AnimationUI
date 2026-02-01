using System;
using UnityEngine;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class SquishTween : TransformTween<Transform, Vector3, Vector3, Vector3>
    {
        // UnityEditor.AnimationCurveWrapperJSON:{"curve":{"serializedVersion":"2","m_Curve":
        // [{"serializedVersion":"3","time":0.0,"value":0.8999999761581421,"inSlope":-1.100000023841858,"outSlope":-1.100000023841858,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":0.09000000357627869,"value":0.8500000238418579,"inSlope":0.0,"outSlope":0.0,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":0.21990416944026948,"value":0.9700000286102295,"inSlope":2.4000000953674318,"outSlope":2.4000000953674318,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":0.2914528548717499,"value":1.3710097074508668,"inSlope":6.699999809265137,"outSlope":6.699999809265137,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":0.38145285844802859,"value":1.511009693145752,"inSlope":-1.8700000047683716,"outSlope":-1.8700000047683716,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":0.46000000834465029,"value":1.090000033378601,"inSlope":-3.6700000762939455,"outSlope":-3.6700000762939455,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":0.5699999928474426,"value":0.9100000262260437,"inSlope":0.0,"outSlope":0.0,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":0.6829984188079834,"value":0.9700000286102295,"inSlope":1.600000023841858,"outSlope":1.600000023841858,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":0.7829984426498413,"value":1.3156213760375977,"inSlope":0.0,"outSlope":0.0,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":1.0,"value":1.0,"inSlope":0.0,"outSlope":0.0,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0}],"m_PreInfinity":2,"m_PostInfinity":2,"m_RotationOrder":4}}
        public AnimationCurve XCurve = new AnimationCurve(){
            keys = new Keyframe[] {
                new Keyframe(0, 0.9f, -1.1f, -1.1f),
                new Keyframe(0.09f, 0.85f, 0, 0),
                new Keyframe(0.22f, 0.97f, 2.4f, 2.4f),
                new Keyframe(0.313f, 1.37f, 6.7f, 6.7f),
                new Keyframe(0.403f, 1.51f, -1.87f, -1.87f),
                new Keyframe(0.46f, 1.09f, -3.67f, -3.67f),
                new Keyframe(0.57f, 0.91f, 0, 0),
                new Keyframe(0.68f, 0.97f, 1.6f, 1.6f),
                new Keyframe(0.78f, 1.32f, 0, 0),
                new Keyframe(1, 1, 0, 0)
            }
        };

        // UnityEditor.AnimationCurveWrapperJSON:{"curve":{"serializedVersion":"2","m_Curve":
        // [{"serializedVersion":"3","time":0.0,"value":1.2965136766433716,"inSlope":4.900000095367432,"outSlope":4.900000095367432,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":0.09000000357627869,"value":1.4965136051177979,"inSlope":0.0,"outSlope":0.0,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":0.22262930870056153,"value":1.026747465133667,"inSlope":-3.740384817123413,"outSlope":-3.740384817123413,"tangentMode":0,"weightedMode":0,"inWeight":0.3333333432674408,"outWeight":0.3333333432674408},{"serializedVersion":"3","time":0.3700000047683716,"value":0.7418884038925171,"inSlope":0.0,"outSlope":0.0,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":0.48641812801361086,"value":0.9667640924453735,"inSlope":4.889529705047607,"outSlope":4.889529705047607,"tangentMode":0,"weightedMode":0,"inWeight":0.3179300129413605,"outWeight":0.3333333432674408},{"serializedVersion":"3","time":0.5792773365974426,"value":1.2735824584960938,"inSlope":0.0,"outSlope":0.0,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0},{"serializedVersion":"3","time":0.815455973148346,"value":0.9007184505462647,"inSlope":1.3183122873306275,"outSlope":1.3183122873306275,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.25231099128723147},{"serializedVersion":"3","time":0.99847412109375,"value":1.0,"inSlope":0.0,"outSlope":0.0,"tangentMode":0,"weightedMode":0,"inWeight":0.0,"outWeight":0.0}],"m_PreInfinity":2,"m_PostInfinity":2,"m_RotationOrder":4}}
        public AnimationCurve YCurve =  new AnimationCurve(){
            keys = new Keyframe[] {
                new Keyframe(0, 1.3f, 4.9f, 4.9f),
                new Keyframe(0.09f, 1.5f, 0, 0),
                new Keyframe(0.22f, 1.03f, -3.74f, -3.74f),
                new Keyframe(0.37f, 0.74f, 0, 0),
                new Keyframe(0.49f, 0.97f, 4.89f, 4.89f),
                new Keyframe(0.58f, 1.27f, 0, 0),
                new Keyframe(0.82f, 0.9f, 1.32f, 1.32f),
                new Keyframe(1, 1, 0, 0)
            }
        };
        public AnimationCurve ZCurve =  new AnimationCurve(){
            keys = new Keyframe[] {
                new Keyframe(0, 1, 0, 0),
                new Keyframe(1, 1, 0, 0)
            }
        };


        protected override Func<Vector3, Vector3, float, Vector3> InterpolationFunction => (a, b, t) => {
            // float x = b.x * XCurve.Evaluate(t);
            // float y = b.y * YCurve.Evaluate(t);
            // float xMul = Mathf.LerpUnclamped(a.x, 1, EaseFunction.Evaluate(t, EaseType));
            // float yMul = Mathf.LerpUnclamped(a.y, 1, EaseFunction.Evaluate(t, EaseType));
            // return new Vector3(XCurve.Evaluate(t) * x * xMul, YCurve.Evaluate(t) * y * yMul, 1);
            float x = b.x * XCurve.Evaluate(t);
            float y = b.y * YCurve.Evaluate(t);
            float z = b.z * ZCurve.Evaluate(t);
            return new Vector3((x-1)*a.x+1, (y-1)*a.y+1, (z-1)*a.z+1);
        };
        public override void ApplyInterpolation(Vector3 value) => Target.localScale = value;
        public override void SetFromAsTargetValue() => From = Vector3.one;
        public override void SetToAsTargetValue() => To = Target.localScale;
    }
}
