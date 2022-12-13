using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[ExecuteInEditMode]
[CustomPropertyDrawer(typeof(Sequence))]
public class SequenceDrawer : PropertyDrawer
{
    float _height = EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight;

    Rect _backgroundRectExtra = new Rect(19, -3, 33, 2);

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if(!property.FindPropertyRelative("IsUnfolded").boolValue)
            return _height;


        //
        Sequence.Type sequenceType = (Sequence.Type)property.FindPropertyRelative("SequenceType").enumValueIndex;
        if(sequenceType == Sequence.Type.Animation)
        {
            Sequence.ObjectType objectType = (Sequence.ObjectType)property.FindPropertyRelative("TargetType").enumValueIndex;
            if(objectType == Sequence.ObjectType.UnityEvent)
            {
                float totalHeight = _height*5 + EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Event")) + EditorGUIUtility.standardVerticalSpacing;
                return totalHeight;
            }

            //
            serializedProperty = property;
            serializedPropertyEnum = property.FindPropertyRelative("TargetRtType");
            SetupIfFirstTime();
            return reorderableList.GetHeight() + _height*10;

            //

            return _height*10;
        }
        else if(sequenceType == Sequence.Type.Wait)
        {
            return _height * 3;
        }
        else if(sequenceType == Sequence.Type.SetActiveAllInput)
        {
            return _height * 3;
        }
        else if(sequenceType == Sequence.Type.SetActive)
        {
            return _height * 4;
        }
        else if(sequenceType == Sequence.Type.SFX)
        {
            return _height * 3;
        }
        else if(sequenceType == Sequence.Type.UnityEvent)
        {
            return _height + (EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Event"))+EditorGUIUtility.singleLineHeight);
        }

        return _height * 7;
    }


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect nextPosition = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        
        Rect backgroundRect = new Rect(_backgroundRectExtra.x, position.y+_backgroundRectExtra.y, position.width+_backgroundRectExtra.width, GetPropertyHeight(property, label)+_backgroundRectExtra.height);
        
        Sequence.Type sequenceType = (Sequence.Type)property.FindPropertyRelative("SequenceType").enumValueIndex;
        if(sequenceType == Sequence.Type.Animation)
            EditorGUI.DrawRect(backgroundRect, new Color(1, 0, 0, 0.1f));
        else if(sequenceType == Sequence.Type.Wait)
            EditorGUI.DrawRect(backgroundRect, new Color(0, 0, 1, 0.1f));
        else if(sequenceType == Sequence.Type.SetActive)
            EditorGUI.DrawRect(backgroundRect, new Color(0, 1, 0, 0.1f));
        else if(sequenceType == Sequence.Type.SetActiveAllInput)
            EditorGUI.DrawRect(backgroundRect, new Color(1, 0, 1, 0.1f));
        else if(sequenceType == Sequence.Type.SFX)
            EditorGUI.DrawRect(backgroundRect, new Color(1, 1, 0, 0.1f));
        else if(sequenceType == Sequence.Type.UnityEvent)
            EditorGUI.DrawRect(backgroundRect, new Color(0, 1, 1, 0.1f));
        
        
        property.FindPropertyRelative("IsUnfolded").boolValue
            = EditorGUI.Foldout(nextPosition, property.FindPropertyRelative("IsUnfolded").boolValue, label);
        if(!property.FindPropertyRelative("IsUnfolded").boolValue)return;

        nextPosition.y += _height;
        EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("SequenceType"), new GUIContent("Type"));

        //
        
        if(sequenceType == Sequence.Type.Animation)
        {
            
            nextPosition.y += _height;
            EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("EaseType"));

            nextPosition.y += _height;
            EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("EasePower"));

            nextPosition.y += _height;
            EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("TargetType")); 

            Sequence.ObjectType objectType = (Sequence.ObjectType)property.FindPropertyRelative("TargetType").enumValueIndex;
            if(objectType == Sequence.ObjectType.UnityEvent)
            {
                nextPosition.y += _height;
                EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("Event"));
                return;
            }
            
            nextPosition.y += _height;
            EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("TargetComp"), new GUIContent("Target"));
            
            nextPosition.y += _height;
            EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("Duration"));

            // nextPosition.y += _height;
            // EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("TargetRtTask"));
            // nextPosition.y += _height;
            // EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("TargetRtTask"));



            //
            EditorGUI.BeginDisabledGroup(serializedPropertyEnum.hasMultipleDifferentValues);
            reorderableList.DoList(nextPosition);
            EditorGUI.EndDisabledGroup();
            // 

            

        }
        else if(sequenceType == Sequence.Type.Wait)
        {
            nextPosition.y += _height;
            EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("Duration"));
        }
        else if(sequenceType == Sequence.Type.SetActiveAllInput)
        {
            nextPosition.y += _height;
            EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("IsActivating"));
        }
        else if(sequenceType == Sequence.Type.SetActive)
        {
            nextPosition.y += _height;
            EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("TargetComp"), new GUIContent("GameObject"));
            nextPosition.y += _height;
            EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("IsActivating"));
        }
        else if(sequenceType == Sequence.Type.SFX)
        {
            nextPosition.y += _height;
            EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("SFX"));
        }
        else if(sequenceType == Sequence.Type.UnityEvent)
        {
            nextPosition.y += _height;
            EditorGUI.PropertyField(nextPosition, property.FindPropertyRelative("Event"));
        }

        


    }









#region EnumFlag
    private string[] enumNames;
    private readonly Dictionary<string, int> enumNameToValue = new Dictionary<string, int>();
    private readonly Dictionary<string, string> enumNameToDisplayName = new Dictionary<string, string>();
    private readonly Dictionary<string, string> enumNameToTooltip = new Dictionary<string, string>();
    private readonly List<string> activeEnumNames = new List<string>();
    private SerializedProperty serializedPropertyEnum;
    private SerializedProperty serializedProperty;
    private ReorderableList reorderableList;
    private bool firstTime = true;
    private Type EnumType
    {
        get { return fieldInfo.FieldType; }
    }
    // public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    // {
    //     serializedProperty = property;
    //     serializedPropertyEnum = property.FindPropertyRelative("TargetRtType");
    //     SetupIfFirstTime();
    //     return reorderableList.GetHeight();
    // }
    private void SetupIfFirstTime()
    {
        if (!firstTime)
        {
            return;
        }
        enumNames = serializedPropertyEnum.enumNames;
        CacheEnumMetadata();
        ParseActiveEnumNames();
        reorderableList = GenerateReorderableList();
        firstTime = false;
    }
    private void CacheEnumMetadata()
    {
        for (var index = 0; index < enumNames.Length; index++)
        {
            enumNameToDisplayName[enumNames[index]] = serializedPropertyEnum.enumDisplayNames[index];
        }
        foreach (string enumName in enumNames)
        {
            enumNameToTooltip[enumName] = EnumType.Name + "." + enumName;
        }
        Sequence.RtType enumValues = serializedPropertyEnum.GetSerializedValue<Sequence.RtType>();
        foreach (string name in enumNames)
        {
            enumNameToValue.Add(name, (int)Enum.Parse(enumValues.GetType(), name));
        }
    }
    // public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    // {
    //     EditorGUI.BeginDisabledGroup(serializedPropertyEnum.hasMultipleDifferentValues);
    //     reorderableList.DoList(position);
    //     EditorGUI.EndDisabledGroup();
    // }
    private ReorderableList GenerateReorderableList()
    {
        return new ReorderableList(activeEnumNames, typeof(string), false, true, true, true)
        {
            drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, new GUIContent(serializedPropertyEnum.displayName, "EnumType: " + EnumType.Name));
            },
            drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                reorderableList.elementHeight = 4*(_height);

                rect.y += 2;
                EditorGUI.LabelField(
                    new Rect(rect.x, rect.y, rect.width, _height),
                    new GUIContent(enumNameToDisplayName[activeEnumNames[index]], enumNameToTooltip[activeEnumNames[index]]),
                    EditorStyles.label);
                
                rect.y += _height;
                if(
                    GUI.Button(
                    new Rect(rect.x, rect.y, rect.width/4-5, _height),
                    "Set Start"
                    )
                )
                {
                    Debug.Log("Set Start");
                }
                EditorGUI.PropertyField(
                    new Rect(rect.x+rect.width/4, rect.y, rect.width*3/4, _height),
                    serializedProperty.FindPropertyRelative("AnchoredPositionStart"), GUIContent.none
                );

                rect.y += _height;
                if(
                    GUI.Button(
                    new Rect(rect.x, rect.y, rect.width/4-5, _height),
                    "Set End"
                    )
                )
                {
                    Debug.Log("Set End");
                }
                EditorGUI.PropertyField(
                    new Rect(rect.x+rect.width/4, rect.y, rect.width*3/4, EditorGUIUtility.singleLineHeight),
                    serializedProperty.FindPropertyRelative("AnchoredPositionEnd"), GUIContent.none
                );

            },
            onAddDropdownCallback = (Rect buttonRect, ReorderableList l) =>
            {
                var menu = new GenericMenu();
                foreach (string enumName in enumNames)
                {
                    if (activeEnumNames.Contains(enumName) == false)
                    {
                        menu.AddItem(new GUIContent(enumNameToDisplayName[enumName]),
                            false, data =>
                            {
                                if (enumNameToValue[(string)data] == 0)
                                {
                                    activeEnumNames.Clear();
                                }
                                activeEnumNames.Add((string)data);
                                SaveActiveValues();
                                ParseActiveEnumNames();
                            },
                            enumName);
                    }
                }
                menu.ShowAsContext();
            },
            onRemoveCallback = l =>
            {
                ReorderableList.defaultBehaviours.DoRemoveButton(l);
                SaveActiveValues();
                ParseActiveEnumNames();
            }
        };
    }
    private void ParseActiveEnumNames()
    {
        activeEnumNames.Clear();
        foreach (string enumValue in enumNames)
        {
            if (IsFlagSet(enumValue))
            {
                activeEnumNames.Add(enumValue);
            }
        }
    }
    private bool IsFlagSet(string enumValue)
    {
        if (enumNameToValue[enumValue] == 0)
        {
            return serializedPropertyEnum.intValue == 0;
        }
        return (serializedPropertyEnum.intValue & enumNameToValue[enumValue]) == enumNameToValue[enumValue];
    }
    private void SaveActiveValues()
    {
        serializedPropertyEnum.intValue = ConvertActiveNamesToInt();
        serializedPropertyEnum.serializedObject.ApplyModifiedProperties();
    }
    private int ConvertActiveNamesToInt()
    {
        return activeEnumNames.Aggregate(0, (current, activeEnumName) => current | enumNameToValue[activeEnumName]);
    }
#endregion EnumFlag
}
