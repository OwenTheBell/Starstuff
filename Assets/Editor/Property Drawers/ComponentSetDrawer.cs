using System;
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
        EditorGUI.indentLevel = 1;
        //property.NextVisible(true);
        //property.NextVisible(true);
        //property.NextVisible(false);
        var set = property.GetValue<ComponentSet>();
        //Debug.Log("property is: " + property.type);
        //if (property.isArray) {
        //    Debug.Log("property is: " + property.arrayElementType);
        //}
        //Debug.Log("children: " + property.CountInProperty());
        //Debug.Log(set.Components.Length);
        SelectContext(position, property, set);
        //var output = "";
        //do {
        //    if (property.isArray)
        //        output += "array of type: " + property.arrayElementType + ", ";
        //} while (property.NextVisible(false));
        //Debug.Log(output);
        //for (var i = 0; i < Contexts.sharedInstance.allContexts.Length; i++) {
        //for (var i = 0; i < 1; i++) {
        //    var context = Contexts.sharedInstance.allContexts[i];
        //    contentPosition = ComponentsForContext(contentPosition, context, property);
        //}
        //for (var i = 0; i < property.arraySize; i++) {
        //    EditorGUI.PropertyField(contentPosition, property.GetArrayElementAtIndex(i));
        //}
        property.SetValue<ComponentSet>(set);
        EditorGUI.indentLevel = savedIndent;
        EditorGUI.EndProperty();
    }

    private Rect SelectContext(Rect pos, SerializedProperty property, ComponentSet set) {
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
        var newIndex = EditorGUI.Popup(pos, label, index, options);
        if (newIndex != index) {
            set.Context = Contexts.sharedInstance.allContexts[newIndex];
        }
        //Debug.Log(options);
        pos.y += 18f;
        return pos;
    }

    private Rect ComponentsForContext(Rect position, IContext context, SerializedProperty property) {
        var components = context.contextInfo.componentNames;
        var label = "Add " + context.GetType().ToString();
        var index = EditorGUI.Popup(position, label, -1, components);
        if (index > 0) {
            Debug.Log("you added the " + components[index] + " component");
            var insertIndex = property.arraySize;
            property.arraySize += 1;
            var component = (IComponent)Activator.CreateInstance(context.contextInfo.componentTypes[index]);
            //property.GetArrayElementAtIndex(insertIndex).objectReferenceValue = component;
            //property.GetArrayElementAtIndex(insertIndex).SetValue<IComponent>(component);
        }
        position.y += 18f;
        return position;
    }
}