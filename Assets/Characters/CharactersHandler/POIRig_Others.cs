using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIRig_Others : POIRig
{
    [Header("Interact FOV Reference")]
    [SerializeField] private Interact interact;

    private void Start()
    {
        interactReference = interact;
    }
}
