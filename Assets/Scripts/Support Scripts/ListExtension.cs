using System;
using System.Collections.Generic;

public static class ListExtensions {

	// run an Action on each member
	public static void Act<T> (this List<T> toMap, Action<T> todo) {
		for (var i = 0; i < toMap.Count; i++) {
			todo(toMap[i]);
		}
	}

	public static List<T> Filter<T> (this List<T> toFilter, Predicate<T> p) {
		var output = new List<T>();
		for (var i = 0; i < toFilter.Count; i++) {
			if (p(toFilter[i])) {
				output.Add(toFilter[i]);
			}
		}
		return output;
	}

    public static void FilterInPlace<T>(this List<T> toFilter, Predicate<T> p) {
        for (var i = toFilter.Count - 1; i >= 0; i--) {
            if (!p(toFilter[i])) {
                toFilter.RemoveAt(i);
            }
        }
    }

    public static List<T> FilterAndRemove<T> (this List<T> toFilter, Predicate<T> p) {
        var output = new List<T>();
        for (var i = toFilter.Count - 1; i >= 0; i--) {
            if (p(toFilter[i])) {
                output.Add(toFilter[i]);
                toFilter.RemoveAt(i);
            }
        }
        return output;
    }

	public static List<T> Map<T> (this List<T> toMap, Func<T, T> todo) {
		var map = new List<T>(toMap.Count);
		for (var i = 0; i < toMap.Count; i++) {
			map.Add(todo(toMap[i]));
		}
		return map;
	}

	public static List<T> Combine<T>(this List<T> list, IEnumerable<T> otherList) {
		var newList = new List<T>(list);
		newList.AddRange(otherList);
		return newList;
	}

    public static bool CheckAllFor<T>(this List<T> toCheck, Predicate<T> todo) {
        foreach (var t in toCheck) if (todo(t)) return true;
        return false;
    }

    public static T Match<T>(this List<T> list, Predicate<T> condition) {
        foreach (var l in list) if (condition(l)) return l;
        return default(T);
    }
}