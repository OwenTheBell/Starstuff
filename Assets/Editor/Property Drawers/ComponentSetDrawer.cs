﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;
using UnityEditor;

[CustomPropertyDrawer(typeof(ComponentSet))]
public class ComponentSetDrawer : PropertyDrawer {

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        var height = 0f;
        foreach (var context in Contexts.sharedInstance.allContexts) {
            height += 18f;
        }
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        label = EditorGUI.BeginProperty(position, label, property);
        //EditorGUI.LabelField(position, new GUIContent(property.name));
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        var savedIndent = EditorGUI.indentLevel;
        position.y += 18f;
        EditorGUI.indentLevel = 0;
        foreach (var context in Contexts.sharedInstance.allContexts) {
            contentPosition = ComponentsForContext(contentPosition, context);
        }
        EditorGUI.indentLevel = savedIndent;
        EditorGUI.EndProperty();
    }

    private Rect ComponentsForContext(Rect position, IContext context) {
        var components = context.contextInfo.componentNames;
        var label = "Add " + context.GetType().ToString();
        var index = EditorGUI.Popup(position, label, -1, components);
        if (index > 0) {
            Debug.Log("you added the " + components[index] + " component");
        }
        position.y += 18f;
        return position;
    }
}