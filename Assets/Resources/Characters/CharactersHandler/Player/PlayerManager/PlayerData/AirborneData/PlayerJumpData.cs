using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerJumpData
{
    [field: SerializeField] public float JumpDecelerationForce { get; private set; } = 1.5f;
    [field: SerializeField] public float RotationTime { get; private set; } = 1f;
    [field: SerializeField] public float JumpForceY { get; private set; } = 5f;
    [field: SerializeField] public float IdleJumpForceMagnitudeXZ { get; private set; } = 0f;
    [field: SerializeField] public float WeakJumpForceMagnitudeXZ { get; private set; } = 1f;
    [field: SerializeField] public float StrongJumpForceMagnitudeXZ { get; private set; } = 2f;
}
