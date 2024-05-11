using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SmoothRigTransition : MonoBehaviour
{
    [SerializeField] private Rig rig;
    [SerializeField] private float soothingSpeed = 1f;
    [SerializeField] private MultiAimConstraint MultiAimConstraint;
    private float targetRigWeight;


    // Start is called before the first frame update
    void Start()
    {
        SetTargetWeight(0f);
    }

    // Update is called once per frame
    void Update()
    {
        rig.weight = Mathf.Lerp(rig.weight, targetRigWeight, Time.deltaTime * soothingSpeed);
    }

    private bool isValidObject()
    {
        return MultiAimConstraint.data.sourceObjects.Count != 0;
    }

    public void ToggleTarget(bool active)
    {
        if (MultiAimConstraint == null || !isValidObject())
            return;
        MultiAimConstraint.data.sourceObjects[0].transform.gameObject.SetActive(active);
    }

    public void SetTargetPosition(Vector3 Position)
    {
        if (!isValidObject())
            return;

        MultiAimConstraint.data.sourceObjects[0].transform.position = Position;
    }

    public void SetTargetWeight(float target)
    {
        targetRigWeight = target;
        ToggleTarget(targetRigWeight != 0);
    }
}
