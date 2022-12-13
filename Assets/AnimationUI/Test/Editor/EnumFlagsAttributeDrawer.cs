// using System;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEditor;
// using UnityEditorInternal;
// using UnityEngine;
// [CustomPropertyDrawer(typeof(Sequence.RtTask))]
// public class EnumFlagsAttributeDrawer : PropertyDrawer
// {
//     float _height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
//     private string[] enumNames;
//     private readonly Dictionary<string, int> enumNameToValue = new Dictionary<string, int>();
//     private readonly Dictionary<string, string> enumNameToDisplayName = new Dictionary<string, string>();
//     private readonly Dictionary<string, string> enumNameToTooltip = new Dictionary<string, string>();
//     private readonly List<string> activeEnumNames = new List<string>();
//     private SerializedProperty serializedPropertyEnum;
//     private SerializedProperty serializedProperty;
//     private ReorderableList reorderableList;
//     private bool firstTime = true;
//     private Type EnumType
//     {
//         get { return fieldInfo.FieldType; }
//     }
//     public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//     {
//         serializedProperty = property;
//         serializedPropertyEnum = property.FindPropertyRelative("TargetRtType");
//         SetupIfFirstTime();
//         return reorderableList.GetHeight();
//     }
//     private void SetupIfFirstTime()
//     {
//         if (!firstTime)
//         {
//             return;
//         }
//         enumNames = serializedPropertyEnum.enumNames;
//         CacheEnumMetadata();
//         ParseActiveEnumNames();
//         reorderableList = GenerateReorderableList();
//         firstTime = false;
//     }
//     private void CacheEnumMetadata()
//     {
//         for (var index = 0; index < enumNames.Length; index++)
//         {
//             enumNameToDisplayName[enumNames[index]] = serializedPropertyEnum.enumDisplayNames[index];
//         }
//         foreach (string enumName in enumNames)
//         {
//             enumNameToTooltip[enumName] = EnumType.Name + "." + enumName;
//         }
//         Sequence.RtTask.RtType enumValues = serializedPropertyEnum.GetSerializedValue<Sequence.RtTask.RtType>();
//         foreach (string name in enumNames)
//         {
//             enumNameToValue.Add(name, (int)Enum.Parse(enumValues.GetType(), name));
//         }
//     }
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     {
//         EditorGUI.BeginDisabledGroup(serializedPropertyEnum.hasMultipleDifferentValues);
//         reorderableList.DoList(position);
//         EditorGUI.EndDisabledGroup();
//     }
//     private ReorderableList GenerateReorderableList()
//     {
//         return new ReorderableList(activeEnumNames, typeof(string), false, true, true, true)
//         {
//             drawHeaderCallback = rect =>
//             {
//                 EditorGUI.LabelField(rect, new GUIContent(serializedPropertyEnum.displayName, "EnumType: " + EnumType.Name));
//             },
//             drawElementCallback = (rect, index, isActive, isFocused) =>
//             {
//                 reorderableList.elementHeight = 4*(_height);

//                 rect.y += 2;
//                 EditorGUI.LabelField(
//                     new Rect(rect.x, rect.y, rect.width, _height),
//                     new GUIContent(enumNameToDisplayName[activeEnumNames[index]], enumNameToTooltip[activeEnumNames[index]]),
//                     EditorStyles.label);
                
//                 rect.y += _height;
//                 if(
//                     GUI.Button(
//                     new Rect(rect.x, rect.y, rect.width/4-5, _height),
//                     "Set Start"
//                     )
//                 )
//                 {
//                     Debug.Log("Set Start");
//                 }
//                 EditorGUI.PropertyField(
//                     new Rect(rect.x+rect.width/4, rect.y, rect.width*3/4, _height),
//                     serializedProperty.FindPropertyRelative("AnchoredPositionStart"), GUIContent.none
//                 );

//                 rect.y += _height;
//                 if(
//                     GUI.Button(
//                     new Rect(rect.x, rect.y, rect.width/4-5, _height),
//                     "Set End"
//                     )
//                 )
//                 {
//                     Debug.Log("Set End");
//                 }
//                 EditorGUI.PropertyField(
//                     new Rect(rect.x+rect.width/4, rect.y, rect.width*3/4, EditorGUIUtility.singleLineHeight),
//                     serializedProperty.FindPropertyRelative("AnchoredPositionEnd"), GUIContent.none
//                 );

//             },
//             onAddDropdownCallback = (Rect buttonRect, ReorderableList l) =>
//             {
//                 var menu = new GenericMenu();
//                 foreach (string enumName in enumNames)
//                 {
//                     if (activeEnumNames.Contains(enumName) == false)
//                     {
//                         menu.AddItem(new GUIContent(enumNameToDisplayName[enumName]),
//                             false, data =>
//                             {
//                                 if (enumNameToValue[(string)data] == 0)
//                                 {
//                                     activeEnumNames.Clear();
//                                 }
//                                 activeEnumNames.Add((string)data);
//                                 SaveActiveValues();
//                                 ParseActiveEnumNames();
//                             },
//                             enumName);
//                     }
//                 }
//                 menu.ShowAsContext();
//             },
//             onRemoveCallback = l =>
//             {
//                 ReorderableList.defaultBehaviours.DoRemoveButton(l);
//                 SaveActiveValues();
//                 ParseActiveEnumNames();
//             }
//         };
//     }
//     private void ParseActiveEnumNames()
//     {
//         activeEnumNames.Clear();
//         foreach (string enumValue in enumNames)
//         {
//             if (IsFlagSet(enumValue))
//             {
//                 activeEnumNames.Add(enumValue);
//             }
//         }
//     }
//     private bool IsFlagSet(string enumValue)
//     {
//         if (enumNameToValue[enumValue] == 0)
//         {
//             return serializedPropertyEnum.intValue == 0;
//         }
//         return (serializedPropertyEnum.intValue & enumNameToValue[enumValue]) == enumNameToValue[enumValue];
//     }
//     private void SaveActiveValues()
//     {
//         serializedPropertyEnum.intValue = ConvertActiveNamesToInt();
//         serializedPropertyEnum.serializedObject.ApplyModifiedProperties();
//     }
//     private int ConvertActiveNamesToInt()
//     {
//         return activeEnumNames.Aggregate(0, (current, activeEnumName) => current | enumNameToValue[activeEnumName]);
//     }
// }