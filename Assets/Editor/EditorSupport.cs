using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;

public static class EditorSupport {
    public static string GetPropertyType(SerializedProperty property) {
        var type = property.type;
        var match = Regex.Match(type, @"PPtr<\$(.*?)>");
        if (match.Success) {
            type = match.Groups[1].Value;
        }
        return type;
    }
}
