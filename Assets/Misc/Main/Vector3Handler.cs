using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Handler
{
    public static Vector3 FindVector(float yawAngleInDegrees, float pitchAngleInDegrees)
    {
        float yawAngle = yawAngleInDegrees * Mathf.Deg2Rad;
        float pitchAngle = pitchAngleInDegrees * Mathf.Deg2Rad;

        float x = Mathf.Sin(yawAngle) * Mathf.Cos(pitchAngle);
        float y = Mathf.Sin(pitchAngle);
        float z = Mathf.Cos(yawAngle) * Mathf.Cos(pitchAngle);

        return new Vector3(x, y, z);
    }

    public static float FindAngleBetweenVectors(Vector2 Vector1, Vector2 Vector2)
    {
        float dotValue = Vector2.Dot(Vector1.normalized, Vector2.normalized);

        return Mathf.Acos(dotValue) * Mathf.Rad2Deg;
    }

    public static Vector3 Vector3Clamp(Vector3 Position, Vector3 minSize, Vector3 maxSize)
    {
        Position.x = Mathf.Clamp(Position.x, minSize.x, maxSize.x);
        Position.y = Mathf.Clamp(Position.y, minSize.y, maxSize.y);
        Position.z = Mathf.Clamp(Position.z, minSize.z, maxSize.z);

        return Position;
    }

    public static float FindAngleByDirection(Vector2 origin, Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - origin;

        return Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    }
}
