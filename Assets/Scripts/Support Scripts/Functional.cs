using System;
using System.Collections.Generic;

public static class Functional {

	public static List<T> Map<T>(List<T> toMap, Func<T, T> todo) {
		var map = new List<T>(toMap.Count);
		for (var i = 0; i < map.Count; i++) {
			map.Add(todo(toMap[i]));
		}
		return map;
	}

}