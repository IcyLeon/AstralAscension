using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactsRingRotation : MonoBehaviour
{
    private Rigidbody rb;
    private float previousRotationDirection;
    private float speed;

    private void Awake()
    {
        speed = 0.15f;
        previousRotationDirection = 1;
        rb = GetComponent<Rigidbody>();
    }
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
    void FixedUpdate()
    {
        UpdateRotation();
    }

    public void DisableRotation()
    {
        enabled = false;
    }

    public void EnableRotation()
    {
        enabled = true;
    }

    public void Rotate(float strength)
    {
        ResetAngularVelocity();
        Vector3 angles = transform.localRotation.eulerAngles;
        angles.y = GetYawAngle(angles.y + strength);
        transform.localRotation = Quaternion.Euler(angles);
    }

    private void OnDisable()
    {
        ResetAngularVelocity();
    }

    private void UpdateRotation()
    {
        float vel = rb.angularVelocity.y;
        float magnitude = Mathf.Abs(vel);

        float direction = Mathf.Sign(vel);

        if (direction == 0)
        {
            direction = previousRotationDirection;
        }
        else
        {
            previousRotationDirection = direction;
        }

        rb.angularVelocity = transform.up * Mathf.Max(speed, magnitude) * direction;

    }

    public void AddRotateTorqueForce(Vector2 direction)
    {
        rb.AddTorque((-transform.up * direction.x), ForceMode.VelocityChange);
    }

    private void ResetAngularVelocity()
    {
        rb.angularVelocity = Vector3.zero;
    }
}
