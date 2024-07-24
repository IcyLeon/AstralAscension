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

    public static float FindAngleBetweenVectors(Vector3 Vector1, Vector3 Vector2)
    {
        float dotValue = Vector3.Dot(Vector1.normalized, Vector2.normalized);

        return Mathf.Acos(dotValue) * Mathf.Rad2Deg;
    }

    public static float FindAngleByDirection(Vector3 origin, Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - origin;

        return Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    }
}
