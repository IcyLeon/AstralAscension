using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimRigData : RigController
{
    [SerializeField] private Transform TargetTransform;

    public void SetTargetPosition(Vector3 Position)
    {
        if (!GetTargetTransform())
            return;

        TargetTransform.position = Position;
    }

    public Transform GetTargetTransform()
    {
        return TargetTransform;
    }

}
