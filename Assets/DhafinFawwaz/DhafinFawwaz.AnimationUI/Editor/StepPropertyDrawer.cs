using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System;

namespace DhafinFawwaz.AnimationUI {
    // [CustomPropertyDrawer(typeof(Tween<,,,>), true)]
    [CustomPropertyDrawer(typeof(Step), true)]
    public class StepPropertyDrawer : PropertyDrawer
    {
        const float BUTTON_WIDTH = 30;
        const float BUTTON_RIGHT_MARGIN = 2;
        const float BUTTON_LEFT_MARGIN = 15;
        const float EASE_TYPE_WIDTH = 102;
        const float EXTRA_EASE_TYPE_WIDTH = 13;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty it = property.Copy();
            var tween = it.managedReferenceValue as ITweenable;
            it.NextVisible(true);
            float depth = it.depth;

            if(tween == null) {
                do {
                    if(it.depth < depth) break;
                    float propHeight = EditorGUI.GetPropertyHeight(it, true);
                    EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, propHeight), it, true);
                    position.y += propHeight + EditorGUIUtility.standardVerticalSpacing;
                } while(it.NextVisible(false));

            } else {
                do {
                    if(it.depth < depth) break;
                    float propHeight = EditorGUI.GetPropertyHeight(it, true);

                    if(it.displayName == "From") {
                        Rect buttonRect = new Rect(position.x + BUTTON_LEFT_MARGIN, position.y, BUTTON_WIDTH, propHeight);
                        if(GUI.Button(buttonRect, "Set")) {
                            tween.SetTargetValueAsFromSafe();
                        }
                        
                        Rect propsRect = new Rect(position.x + BUTTON_WIDTH + BUTTON_RIGHT_MARGIN, position.y, position.width - BUTTON_WIDTH - BUTTON_RIGHT_MARGIN, propHeight);
                        float defaultLabelWidth = EditorGUIUtility.labelWidth;
                        EditorGUIUtility.labelWidth -= (BUTTON_WIDTH + BUTTON_RIGHT_MARGIN);
                        EditorGUI.PropertyField(propsRect, it, true);
                        EditorGUIUtility.labelWidth = defaultLabelWidth;
                    } else if(it.displayName == "To") {
                        Rect buttonRect = new Rect(position.x + BUTTON_LEFT_MARGIN, position.y, BUTTON_WIDTH, propHeight);
                        if(GUI.Button(buttonRect, "Set")) {
                            tween.SetTargetValueAsToSafe();
                        }   
                        
                        Rect propsRect = new Rect(position.x + BUTTON_WIDTH + BUTTON_RIGHT_MARGIN, position.y, position.width - BUTTON_WIDTH - BUTTON_RIGHT_MARGIN, propHeight);
                        float defaultLabelWidth = EditorGUIUtility.labelWidth;
                        EditorGUIUtility.labelWidth -= (BUTTON_WIDTH + BUTTON_RIGHT_MARGIN);
                        EditorGUI.PropertyField(propsRect, it, true);
                        EditorGUIUtility.labelWidth = defaultLabelWidth;
                    } else if(it.displayName == "Duration") {
                        Rect propsRect = new Rect(position.x, position.y, position.width - EASE_TYPE_WIDTH, propHeight);
                        EditorGUI.PropertyField(propsRect, it, true);
                        it.NextVisible(false);
                        Rect easeTypeRect = new Rect(position.x + position.width - EASE_TYPE_WIDTH - EXTRA_EASE_TYPE_WIDTH, position.y, EASE_TYPE_WIDTH + EXTRA_EASE_TYPE_WIDTH, propHeight);
                        EditorGUI.PropertyField(easeTypeRect, it, GUIContent.none);
                    } else {
                        Rect rect = new Rect(position.x, position.y, position.width, propHeight);
                        EditorGUI.PropertyField(rect, it, true);
                    }

                    position.y += propHeight + EditorGUIUtility.standardVerticalSpacing;
                } while(it.NextVisible(false));

            }

            
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUI.GetPropertyHeight(property, true);
            var tween = property.managedReferenceValue as ITweenable;
            if(tween != null) height -= EditorGUIUtility.singleLineHeight;
            else height -= EditorGUIUtility.singleLineHeight/2;
            return height;

            // float height = 0;
            // SerializedProperty it = property.Copy();
            // var tween = it.managedReferenceValue as IUpdatable;
            // it.NextVisible(true);
            // float depth = it.depth;
            // do {
            //     if(it.depth < depth) break;
            //     height += EditorGUI.GetPropertyHeight(it, true) + EditorGUIUtility.standardVerticalSpacing;
            // } while(it.NextVisible(false));

            // if(tween != null) {
            //     height -= (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
            // }
            // return height;
            
        }
    }
}