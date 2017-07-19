using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using EditorTools;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor {

    public override void OnInspectorGUI() {
        serializedObject.Update();
        var property = serializedObject.GetIterator();
        Layout.DisplayScript(property);

        // step into the child
        property.NextVisible(true);
        while (property.NextVisible(false)) {
            if (property.isArray &&
                Support.CleanType(property.arrayElementType) == typeof(GenerateFeature).ToString()
            ) {
                Layout.EditableArray(property);
            }
            else {
                EditorGUILayout.PropertyField(property);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

}
