using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerPlungeData
{
    [field: SerializeField] public float PlungeSpeed { get; private set; } = 10f;
    [field: SerializeField] public float GroundCheckDistance { get; private set; } = 10f;
}
