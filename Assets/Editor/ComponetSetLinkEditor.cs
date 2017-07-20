using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using EditorTools;

[CustomEditor(typeof(ComponentSetLink))]
public class ComponentSetLinkEditor : Editor {

    public override void OnInspectorGUI() {
        serializedObject.Update();
        var setLink = (ComponentSetLink)target;
        setLink.Test = EditorGUILayout.IntField("testing", setLink.Test);
        //Layout.DrawComponentSet(setLink.Set);
        serializedObject.ApplyModifiedProperties();
    }

}
