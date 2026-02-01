using System;
using UnityEngine;
using TMPro;
using System.Text;
using System.Collections.Generic;

namespace DhafinFawwaz.AnimationUI {

    [Serializable]
    public class TextGlitchRevealTween : TMPTween<TMP_Text, string, string, string>
    {
        public string PossibleCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        int CountDifference(string a, string b) {
            int leastLength = Mathf.Min(a.Length, b.Length);
            int count = 0;
            for (int i = 0; i < leastLength; i++) {
                if (a[i] != b[i]) count++;
            }
            return count + Mathf.Abs(a.Length - b.Length);
        }
        void AppendSpaceOrRemoveUntilLength(ref StringBuilder sb, int length) {
            while(sb.Length < length) {
                sb.Append(" ");
            }
            while(sb.Length > length) {
                sb.Remove(sb.Length-1, 1);
            }
        }
        protected override Func<string, string, float, string> InterpolationFunction => (a, b, t) => {
            if(Target == null) return a;
            if(t == 0) {
                StringBuilder sa = new StringBuilder(a);
                AppendSpaceOrRemoveUntilLength(ref sa, b.Length);
                Target.text = sa.ToString();
            }

            int maxDifference = CountDifference(a, b);
            int currentDifference = CountDifference(Target.text, b);
            int supposedDifference = Mathf.FloorToInt(maxDifference * (1-t));
            int delta = currentDifference - supposedDifference;

            for(int i = 0; i < delta; i++) {
                Target.text = Fix1Char(Target.text, b);
            }
            for(int i = 0; i > delta; i--) {
                Target.text = Broke1Char(Target.text, b, a);
            }

            Target.text = RandomlyChangeEveryDifferentChar(Target.text, b);

            return Target.text;
        };

        string Fix1Char(string a, string b) {
            List<(int, char)> diffIndexes = new List<(int, char)>();
            for (int i = 0; i < b.Length; i++) {
                if (a[i] != b[i]) {
                    diffIndexes.Add((i, b[i]));
                }
            }
            if(diffIndexes.Count == 0) return a;
            int rand = UnityEngine.Random.Range(0, diffIndexes.Count);
            int idx = diffIndexes[rand].Item1;
            char c = diffIndexes[rand].Item2;

            return a.Substring(0, idx) + c + a.Substring(idx+1);
        }
        string Broke1Char(string a, string b, string original) {
            List<(int, char)> sameIndexes = new List<(int, char)>();
            for (int i = 0; i < b.Length; i++) {
                if (a[i] == b[i]) {
                    sameIndexes.Add((i, b[i]));
                }
            }
            if(sameIndexes.Count == 0) return a;
            int rand = UnityEngine.Random.Range(0, sameIndexes.Count);
            int idx = sameIndexes[rand].Item1;
            char c = original[idx];

            return a.Substring(0, idx) + c + a.Substring(idx+1);
        }

        string RandomlyChangeEveryDifferentChar(string a, string b) {
            StringBuilder sb = new StringBuilder(a);
            for (int i = 0; i < b.Length; i++) {
                if (a[i] != b[i]) {
                    sb[i] = RandomChar();
                }
            }
            return sb.ToString();
        }
        char RandomChar() => PossibleCharacters[UnityEngine.Random.Range(0, PossibleCharacters.Length)];

        public override void ApplyInterpolation(string value) => Target.text = value;
        public override void SetFromAsTargetValue() => From = Target.text;
        public override void SetToAsTargetValue() => To = Target.text;

#if UNITY_EDITOR
        TextGlitchRevealTween(){
            UnityEditor.EditorApplication.delayCall += () => {
                SetEase(Ease.Linear);
            };
        }
#endif
    }
}
