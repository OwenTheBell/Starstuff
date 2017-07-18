using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using UnityEditor;

[CustomEditor(typeof(GenerateFeature))]
public class GenerateFeatureEditor : Editor {

    static GUIContent addButton = new GUIContent("+", "add system");
    static GUIContent removeButton = new GUIContent("-", "remove system");
    static GUIContent moveUpButton = new GUIContent("\u02c4", "move up");
    static GUIContent moveDownButton = new GUIContent("\u02c5", "move up");

    public override void OnInspectorGUI() {
        serializedObject.Update();
        var property = serializedObject.GetIterator();
        property.NextVisible(true);
        while (property.NextVisible(false)) {
            if (property.name == "Systems") {
                CreateSystemField(property);
            }
            else {
                EditorGUILayout.PropertyField(property);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

    void CreateSystemField(SerializedProperty property) {
        //var oldPropertyState = property.Copy();
        EditorGUILayout.LabelField("Systems");
        EditorGUI.indentLevel += 1;
        for (var i = 0; i < property.arraySize; i++) {
            var childProperty = property.GetArrayElementAtIndex(i);
            if (EditorSupport.GetPropertyType(childProperty) == typeof(SystemGenerator).ToString()) {
                EditorGUILayout.PropertyField(childProperty, GUIContent.none);
            }
        }
        if (GUILayout.Button(addButtonContent)) {
            property.InsertArrayElementAtIndex(property.arraySize);
        }
        EditorGUI.indentLevel -= 1;
        //property = oldPropertyState;
    }

}
