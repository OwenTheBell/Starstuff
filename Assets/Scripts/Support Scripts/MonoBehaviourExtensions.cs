using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions {
	public static bool HasComponent<T>(this MonoBehaviour behaviour) {
		return behaviour.GetComponent<T>() != null;
	}

	public static bool HasComponentInChildren<T>(this MonoBehaviour behaviour) {
		return behaviour.GetComponentInChildren<T>() != null;
	}
}