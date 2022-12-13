using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationUI : MonoBehaviour
{
    public Sequence[] AnimationSequence;
    public void Play() => StartCoroutine(PlayAnimation());
    public void PlayReversed() => StartCoroutine(PlayAnimationReversed());
    IEnumerator PlayAnimation()
    {
        foreach(Sequence sequence in AnimationSequence)
        {
            switch(sequence.SequenceType)
            {
                case Sequence.Type.Animation:
                    // StartCoroutine(EaseOutRt(sequence.TargetRt, sequence.StartPosition, sequence.EndPosition, sequence.Duration));
                    break;
                
                case Sequence.Type.Wait:
                    yield return new WaitForSecondsRealtime(sequence.Duration);
                    break;

                case Sequence.Type.SetActiveAllInput:
                    Singleton.Instance.Game.SetActiveAllInput(sequence.IsButtonBlocking);
                    break;

                case Sequence.Type.SetActive:
                    sequence.Target.SetActive(sequence.IsActivating);
                    break;

                case Sequence.Type.SFX:
                    Singleton.Instance.Audio.PlaySound(sequence.SFX);
                    break;
            }
        }
    }

    IEnumerator EaseOutRt(RectTransform Rt, Vector2 startPosition, Vector2 targetPosition, float duration)
    {
        float t = 0;
        while (t <= 1)
        {
            t += Time.unscaledDeltaTime/duration;
            Rt.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, Ease.InOutQuart(t));
            yield return null;
        }
        Rt.anchoredPosition = targetPosition;
    }



    IEnumerator PlayAnimationReversed()
    {
        yield return null;
    }



#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
            UnityEditor.SceneView.RepaintAll();
        }
    }
    void Reset() //For the default value. A hacky way because the inspector reset the value for Serialized class
    {
        AnimationSequence = new Sequence[1]
        {
            new Sequence()
        };
    }
#endif
}
