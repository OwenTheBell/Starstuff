using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(IComponent))]
public class ComponentDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        label = EditorGUI.BeginProperty(position, label, property);
        EditorGUI.EndProperty();
    }
}
