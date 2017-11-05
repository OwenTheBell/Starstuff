using UnityEngine;

public static class Vector2Extensions {
    public static float AngleToPoint(this Vector2 center, Vector2 point) {
        return Mathf.Atan2(point.y - center.y, point.x - center.x);
    }
}
