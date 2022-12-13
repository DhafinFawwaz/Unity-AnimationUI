using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(AnimationUI))]
public class AnimationUIInspector : Editor
{
    public override void OnInspectorGUI()
    {
        AnimationUI animationUI = (AnimationUI)target;
        if(animationUI.AnimationSequence == null) //Prevent error when adding component
        {
            DrawDefaultInspector();
            return;
        }

        float _currentTime = 0;
        foreach(Sequence sequence in animationUI.AnimationSequence)
        {
            sequence.AtTime = "At "+_currentTime.ToString() + "s";

            if(sequence.SequenceType == Sequence.Type.Animation)
            {
                if(sequence.TargetComp != null)
                {
                    sequence.AtTime += " ["+sequence.TargetComp.name+"]";
                    if(sequence.TargetType == Sequence.ObjectType.Automatic)
                    {
                        if(sequence.TargetComp.GetComponent<RectTransform>() != null)sequence.AtTime += " [RectTransform]";
                        else if(sequence.TargetComp.transform != null)sequence.AtTime += " [Transform]";
                    }
                    else if(sequence.TargetType == Sequence.ObjectType.RectTransform)
                    {
                        if(sequence.TargetComp.GetComponent<RectTransform>() != null)sequence.AtTime += " [RectTransform]";
                        else
                        {
                            sequence.TargetComp = null;
                            // sequence.AtTime += " [Unassigned] [RectTransform]";
                        }
                    }
                    else if(sequence.TargetType == Sequence.ObjectType.Image)
                    {
                        if(sequence.TargetComp.GetComponent<Image>() != null)sequence.AtTime += " [Image]";
                        else
                        {
                            sequence.TargetComp = null;
                            // sequence.AtTime += " [Unassigned] [Image]";
                        }
                    }
                    else if(sequence.TargetType == Sequence.ObjectType.Transform)
                    {
                        if(sequence.TargetComp.transform != null)sequence.AtTime += " [Transform]";
                        else
                        {
                            sequence.TargetComp = null;
                            // sequence.AtTime += " [Unassigned] [Transform]";
                        }
                    }
                    else if(sequence.TargetType == Sequence.ObjectType.UnityEvent)
                    {
                        sequence.AtTime += " [UnityEvent]";
                    }
                }
                else // if TargetComp isn't assigned in inspector
                {
                    if(sequence.TargetType == Sequence.ObjectType.Automatic)
                        sequence.AtTime += " [Unassigned] [Animation]";
                    else if(sequence.TargetType == Sequence.ObjectType.RectTransform)
                        sequence.AtTime += " [Unassigned] [RectTransform]";
                    else if(sequence.TargetType == Sequence.ObjectType.Transform)
                        sequence.AtTime += " [Unassigned] [Transform]";
                    else if(sequence.TargetType == Sequence.ObjectType.Image)
                        sequence.AtTime += " [Unassigned] [Image]";
                    else if(sequence.TargetType == Sequence.ObjectType.UnityEvent)
                        sequence.AtTime += " [UnityEvent]";
                }
            }

            else if(sequence.SequenceType == Sequence.Type.Wait)
            {
                _currentTime += sequence.Duration;
                sequence.StartTime = _currentTime;
                sequence.AtTime += " [Wait "+sequence.Duration+"s]";
            }
            else if(sequence.SequenceType == Sequence.Type.SetActiveAllInput)
            {
                sequence.AtTime += " [SetActiveAllInput to "+sequence.IsActivating+"]";
            }
            else if(sequence.SequenceType == Sequence.Type.SetActive)
            {
                if(sequence.TargetComp != null)
                {
                    sequence.AtTime += " ["+sequence.TargetComp.name+"] [SetActive to "+sequence.IsActivating+"]";
                }
                else // if TargetComp isn't assigned in inspector
                {
                    sequence.AtTime += " [Unassigned] [SetActive to "+sequence.IsActivating+"]";
                }
            }
            else if(sequence.SequenceType == Sequence.Type.SFX)
            {
                if(sequence.SFX != null)
                    sequence.AtTime += " ["+sequence.SFX.name+"] [SFX]";
                else // if SFX isn't assigned in inspector
                    sequence.AtTime += " [Unassigned] [SFX]";
            }
            else if(sequence.SequenceType == Sequence.Type.UnityEvent)
            {
                sequence.AtTime += " [Custom] [UnityEvent]";
            }
        }

        DrawDefaultInspector();
    }

}
