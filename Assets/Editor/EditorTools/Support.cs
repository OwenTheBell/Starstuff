using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace EditorTools {
    public static class Support {
        public static string CleanType(SerializedProperty property) {
            return CleanType(property.type);
        }

        public static string CleanType(string type) {
            var match = Regex.Match(type, @"PPtr<\$(.*?)>");
            var altMatch = Regex.Match(type, @"PPtr<(.*?)>");
            if (match.Success) {
                type = match.Groups[1].Value;
            }
            else if (altMatch.Success) {
                type = altMatch.Groups[1].Value;
            }
            return type;
        }
    }
}
