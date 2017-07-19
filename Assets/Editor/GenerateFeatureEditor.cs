using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using UnityEditor;

[CustomEditor(typeof(GenerateFeature))]
public class GenerateFeatureEditor : Editor {

    static GUIContent addButton = new GUIContent("+", "add system");
    static GUIContent removeButton = new GUIContent("X", "remove system");
    static GUIContent moveUpButton = new GUIContent("\u02c4", "move up");
    static GUIContent moveDownButton = new GUIContent("\u02c5", "move up");

    public override void OnInspectorGUI() {
        serializedObject.Update();
        var property = serializedObject.GetIterator();
        property.NextVisible(true);
        while (property.NextVisible(false)) {
            if (property.isArray &&
                EditorSupport.CleanType(property.arrayElementType) == typeof(SystemGenerator).ToString()
            ) {
                EditorSupport.EditableArray(property);
            }
            else {
                EditorGUILayout.PropertyField(property);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}