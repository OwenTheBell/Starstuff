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
}