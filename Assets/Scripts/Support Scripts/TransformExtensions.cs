using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions {

	public static Transform[] GetAllChildren(this Transform t) {
		return RecursiveGetAllWhere(t, new List<Transform>(), (Transform tran) => {return true;})();
	}

	public static Transform[] GetAllChildrenWhere(
		this Transform transform,
		Predicate<Transform> p
	) {
		return RecursiveGetAllWhere(transform, new List<Transform>(), p)();	
	}

	private static Func<Transform[]> RecursiveGetAllWhere(
		Transform t,
		List<Transform> l,
		Predicate<Transform> p
	) {
		if (p(t)) {
			l.Add(t);
		}
		for (int i = 0; i < t.childCount; i++) {
			RecursiveGetAllWhere(t.GetChild(i), l, p);
		}
		return () => { return l.ToArray(); };
	}

    public static Quaternion DirectionTo(this Transform t, Transform other) {
        var savedRotation = t.rotation;
        t.LookAt(other);
        var direction = t.rotation;
        t.rotation = savedRotation;
        return direction;
    }
}
