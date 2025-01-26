using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using MackySoft.SerializeReferenceExtensions.Editor;

namespace DhafinFawwaz.AnimationUI
{

	[CustomPropertyDrawer(typeof(SubclassSelectorAnimationUIAttribute))]
	public class SubclassSelectorAnimationUIDrawer : SubclassSelectorDrawer {

        const float BG_LEFT_PADDING = 28;
        const float BG_RIGHT_PADDING = 5;
		const float BG_TOP_PADDING = 0;
		const float BG_BOTTOM_PADDING = 0;
		const float PROPS_MOVE_LEFT = 15;
		const float SELECTOR_LEFT_MARGIN = 70;

		
        
		public static void DrawBackground(Rect position, SerializedProperty it) {
            Color color = new Color(1,0,0,0.0f);
            var prop = it.managedReferenceValue; 

			// prop will be null after undoing
            if(prop != null && DhafinFawwaz.AnimationUI.BGColorAttribute.TryFindThisOrAnyParentContainBGColorAttribute(prop.GetType(), out DhafinFawwaz.AnimationUI.BGColorAttribute attr)) {
                color = attr.Color;
            }

            EditorGUI.DrawRect(new Rect(
                position.x-BG_LEFT_PADDING,
                position.y-BG_TOP_PADDING,
                position.width+BG_LEFT_PADDING+BG_RIGHT_PADDING,
                position.height+BG_TOP_PADDING+BG_BOTTOM_PADDING
            ), color);


        }

		const float PROGRESS_LEFT_MARGIN = -28;
		const float PROGRESS_TOP_PADDING = 0;
		const float PROGRESS_WIDTH = 3;
		// const float PROGERSS_WIDTH_HORIZONTAL_RIGHT_PADDING = 33;
		public static void DrawProgressOnTheLeftSide(Rect position, SerializedProperty it) {
			if (it == null || it.managedReferenceValue == null) return; // it.managedReferenceValue will be null after undoing
			Color color = new Color(0,1,0,0.2f);
			var prop = it.managedReferenceValue as Step;
			float progress = prop.EditorOnlyProgress;
			// Debug.Log(prop._name);
			// Debug.Log(progress);

			// Vertical
			EditorGUI.DrawRect(new Rect(
				position.x + PROGRESS_LEFT_MARGIN,
				position.y-PROGRESS_TOP_PADDING,
				PROGRESS_WIDTH,
				(position.height+PROGRESS_TOP_PADDING) * progress
			), color);

			// Horizontal
			// EditorGUI.DrawRect(new Rect(
			// 	position.x + PROGRESS_LEFT_MARGIN,
			// 	position.y-PROGRESS_TOP_PADDING,
			// 	(position.width + PROGERSS_WIDTH_HORIZONTAL_RIGHT_PADDING) * progress,
			// 	PROGRESS_WIDTH
			// ), color);
		}


		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			DrawBackground(position, property);
			base.OnGUI(position, property, label);

			position.height = GetPropertyHeight(property, label);
			DrawProgressOnTheLeftSide(position, property);
		}

	}
}
