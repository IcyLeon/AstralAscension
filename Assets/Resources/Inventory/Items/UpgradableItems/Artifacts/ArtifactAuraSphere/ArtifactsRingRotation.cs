using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactsRingRotation : MonoBehaviour
{
    private float GetYawAngle(float Angle)
    {
        float actualAngle = Angle;

        if (actualAngle > 360f)
            actualAngle -= 360f;

        if (actualAngle < 0f)
            actualAngle += 360f;

        return actualAngle;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angles = transform.localRotation.eulerAngles;
        angles.y = GetYawAngle(angles.y + Time.unscaledDeltaTime * 10f);
        transform.localRotation = Quaternion.Euler(angles);
    }
}
