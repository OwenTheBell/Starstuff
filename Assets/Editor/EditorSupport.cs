using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public static class EditorSupport {

    static GUIContent addButton = new GUIContent("+", "add system");
    static GUIContent removeButton = new GUIContent("X", "remove system");
    static GUIContent moveUpButton = new GUIContent("\u02c4", "move up");
    static GUIContent moveDownButton = new GUIContent("\u02c5", "move up");

    public static string CleanType(SerializedProperty property) {
        return CleanType(property.type);
    }

    public static string CleanType(string type) {
        var match = Regex.Match(type, @"PPtr<\$(.*?)>");
        if (match.Success) {
            type = match.Groups[1].Value;
        }
        return type;
    }

    public static bool EditableArray(SerializedProperty property, string name = "") {
        if (!property.isArray) return false;

        var toMoveUp = -1;
        var toMoveDown = -1;
        var toRemove = -1;

        if (name == "") name = property.displayName;
        EditorGUILayout.LabelField(name);
        EditorGUI.indentLevel += 2;
        for (var i = 0; i < property.arraySize; i++) {
            var childProperty = property.GetArrayElementAtIndex(i);
            if (childProperty.type == property.arrayElementType) {
				EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(childProperty, GUIContent.none);
                if (GUILayout.Button(moveUpButton)) toMoveUp = i;
                if (GUILayout.Button(moveDownButton)) toMoveDown = i;
                if (GUILayout.Button(removeButton)) toRemove = i;
				EditorGUILayout.EndHorizontal();
            }
        }
        if (toMoveUp > 0) {
            property.MoveArrayElement(toMoveUp, toMoveUp - 1);
        }
        else if (toMoveDown > -1 && toMoveDown < property.arraySize - 1) {
            property.MoveArrayElement(toMoveDown, toMoveDown + 1);
        }
        else if (toRemove >= 0) {
            // move the removed element to the end and resize the array
            // removing this way ensures that you are not left with empty slots
            property.MoveArrayElement(toRemove, property.arraySize - 1);
            property.arraySize -= 1;
        }
        if (GUILayout.Button(addButton)) {
            property.InsertArrayElementAtIndex(property.arraySize);
        }
        EditorGUI.indentLevel -= 2;

        return true;
    }
}
