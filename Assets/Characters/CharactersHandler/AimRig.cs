using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimRig : MonoBehaviour
{
    [SerializeField] private Rig rig;
    [SerializeField] private float soothingSpeed = 1f;
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

    public void SetTargetWeight(float target)
    {
        targetRigWeight = target;
    }
}
