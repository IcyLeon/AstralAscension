using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Rig))]
public class RigController : MonoBehaviour
{
    private Rig rig;
    [SerializeField] private float soothingWeightSpeed = 1f;
    private float targetRigWeight;

    private void Awake()
    {
        rig = GetComponent<Rig>();
        SetTargetWeight(0f);
    }

    void Update()
    {
        SmoothRigWeight();
    }


    private void SmoothRigWeight()
    {
        rig.weight = Mathf.Lerp(rig.weight, targetRigWeight, Time.deltaTime * soothingWeightSpeed);
    }

    public void SetTargetWeight(float target)
    {
        targetRigWeight = target;
    }
}
