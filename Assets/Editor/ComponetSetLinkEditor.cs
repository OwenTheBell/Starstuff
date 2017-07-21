using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using EditorTools;
using Entitas;
using Entitas.VisualDebugging.Unity.Editor;

[CustomEditor(typeof(ComponentSet))]
public class ComponentSetLinkEditor : Editor {

    private void Awake() {
        LoadData();
    }

    private void OnDisable() {
        SaveData();
    }

    public void LoadData() {
        var set = (ComponentSet)target;

        //if (set.Test == null) return;

        //var output = "";
        //foreach (var i in set.Test) {
        //    output += i + ", ";
        //}
        //Debug.Log(output);
        //if (set.SavedValues == null) {
        //    set.SavedValues = new Dictionary<FieldInfo, object>();
        //    return;
        //}
        if (set.Fields == null) {
            Debug.Log("making save slots");
            set.Fields = new List<FieldInfo>();
            set.Values = new List<object>();
            return;
        }
        var type = typeof(ComponentSet);
        var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Default;
        var fields = type.GetFields(flags);
        var output = "";
        foreach (var f in fields) {
            if (f.Name == "Values" || f.Name == "Fields") continue;
            var index = set.Fields.IndexOf(f);
            if (index >= 0) {
                output += f.Name;
            }
            //f.SetValue(set, set.SavedValues[f]);
        }
        Debug.Log(output);
    }

    public void SaveData() {
        var set = (ComponentSet)target;
        if (set.Fields == null) {
            set.Fields = new List<FieldInfo>();
            set.Values = new List<object>();
        }
        set.Fields.Clear();
        set.Values.Clear();
        //set.SavedValues.Clear();
        var type = typeof(ComponentSet);
        var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Default;
        var fields = type.GetFields(flags);
        //var output = "";
        foreach (var f in fields) {
            if (f.Name == "Values" || f.Name == "Fields") continue;
            //set.SavedValues.Add(f, f.GetValue(set));
            set.Fields.Add(f);
            set.Values.Add(f.GetValue(set));
        }
        //Debug.Log(output);
        //set.Test = new List<int>(new int[] { 1, 2, 3, 4, 5 });
    }

    public override void OnInspectorGUI() {
        var set = (ComponentSet)target;
        set.Context = SelectContext(set.Context);
        //set.Test = EditorGUILayout.IntField("test", set.Test);
        //serializedObject.Update();
        //setLink.Test = EditorGUILayout.IntField("testing", setLink.Test);
        //Layout.DrawComponentSet(setLink.Set);
        //serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);
    }

    private IContext SelectContext(IContext currentContext) {
        var contexts = Contexts.sharedInstance.allContexts;
        var index = -1;
        var options = new string[contexts.Length];
        for (var i = 0; i < contexts.Length; i++) {
            options[i] = contexts[i].GetType().ToString();
            if (contexts[i] == currentContext) {
                index = i;
            }
        }
        var label = "Context:";
        var newIndex = EditorGUILayout.Popup(label, index, options);
        if (newIndex != index) {
            return Contexts.sharedInstance.allContexts[newIndex];
        }
        return currentContext;
    }

}
