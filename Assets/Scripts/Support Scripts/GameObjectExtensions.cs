using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions {
	public static bool HasComponent<T>(this GameObject gameObject) {
		return gameObject.GetComponent<T>() != null;
	}
	public static bool HasComponentInChildren<T>(this GameObject gameObject) {
		return gameObject.GetComponentInChildren<T>() != null;
	}
    public static List<GameObject> ListOfChildrenWithComponent<T>(this GameObject gameObject) where T : Component {
        var output = new List<GameObject>();
        foreach (var child in gameObject.GetComponentsInChildren<T>()) {
            output.Add(child.gameObject);
        }
        return output;
    }
}