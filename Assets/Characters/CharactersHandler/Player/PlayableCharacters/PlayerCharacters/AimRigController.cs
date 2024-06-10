using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SmoothRigTransition))]
public class AimRigController : MonoBehaviour
{
    public SmoothRigTransition SmoothRigTransition { get; private set; }

    private void Awake()
    {
        SmoothRigTransition = GetComponent<SmoothRigTransition>();
    }
}
