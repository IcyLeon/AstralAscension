using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Rig))]
public class SmoothRigTransition : MonoBehaviour
{
    private Rig rig;
    [SerializeField] private float soothingSpeed = 1f;
    [SerializeField] private MultiAimConstraint MultiAimConstraint;
    [SerializeField] private Transform TargetTransform;
    private Vector3 targetPosition;
    private float targetRigWeight;

    private void Awake()
    {
        rig = GetComponent<Rig>();
        SetTargetWeight(0f);
    }

    // Update is called once per frame
    void Update()
    {
        rig.weight = Mathf.Lerp(rig.weight, targetRigWeight, Time.deltaTime * soothingSpeed);
        UpdateTargetPosition();
    }

    public void ToggleTarget(bool active)
    {
        if (!GetTargetTransform())
            return;

        TargetTransform.gameObject.SetActive(active);
    }

    public void SetTargetPosition(Vector3 Position)
    {
        if (!GetTargetTransform())
            return;

        targetPosition = Position;
    }

    private void UpdateTargetPosition()
    {
        TargetTransform.position = Vector3.Lerp(TargetTransform.position, targetPosition, Time.deltaTime * soothingSpeed);
    }

    public Transform GetConstraintObject()
    {
        return MultiAimConstraint.data.constrainedObject;
    }

    public Transform GetTargetTransform()
    {
        return TargetTransform;
    }

    public void SetTargetWeight(float target)
    {
        targetRigWeight = target;
        ToggleTarget(targetRigWeight != 0);
    }
}
