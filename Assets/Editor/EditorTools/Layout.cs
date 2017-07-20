using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace EditorTools {
    public static class Layout {

        static GUIContent addButton = new GUIContent("+", "add system");
        static GUIContent removeButton = new GUIContent("X", "remove system");
        static GUIContent moveUpButton = new GUIContent("\u02c4", "move up");
        static GUIContent moveDownButton = new GUIContent("\u02c5", "move up");

        public static void DisplayScript(SerializedProperty property) {
            // clone the property so we don't mess with it's original state
            var cloned = property.Copy();
            cloned.NextVisible(true);
            if (Support.CleanType(cloned) == "MonoScript") {
                GUI.enabled = false;
                EditorGUILayout.PropertyField(cloned);
                GUI.enabled = true;
            }
        }

        public static bool EditableArray(SerializedProperty property, string name = "") {
            if (!property.isArray) return false;

            var toMoveUp = -1;
            var toMoveDown = -1;
            var toRemove = -1;

            var savedColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
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
            GUI.backgroundColor = savedColor;
            EditorGUI.indentLevel -= 2;

            return true;
        }

        public static void DrawComponentSet(ComponentSet set) {
            var contexts = Contexts.sharedInstance.allContexts;
            var index = -1;
            var options = new string[contexts.Length];
            for (var i = 0; i < contexts.Length; i++) {
                options[i] = contexts[i].GetType().ToString();
                if (contexts[i] == set.Context) {
                    index = i;
                }
            }
            var label = "Context:";
            var newIndex = EditorGUILayout.Popup(label, index, options);
            if (newIndex != index) {
                set.Context = contexts[newIndex];
            }
        }
    }
}