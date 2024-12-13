using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIRig_Others : POIRig
{
    [Header("Interact FOV Reference")]
    [SerializeField] private InteractSensor InteractSensor;

    protected override void Awake()
    {
        base.Awake();
        interactSensorReference = InteractSensor;
    }
}
