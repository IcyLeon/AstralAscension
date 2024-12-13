using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerFallData
{
    [field: SerializeField] public float FallLimitVelocity { get; private set; } = 15f;
}
