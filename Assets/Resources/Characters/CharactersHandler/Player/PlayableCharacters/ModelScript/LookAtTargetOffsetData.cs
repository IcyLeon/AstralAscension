using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTargetOffsetData : MonoBehaviour
{
    [SerializeField] private Transform Start;
    [SerializeField] private Transform End;

    public float GetDistanceBetweenTargets()
    {
        return Vector3.Magnitude(Start.position - End.position);
    }
}
