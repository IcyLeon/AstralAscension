using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GroundedData
{
    [field: SerializeField] public float BaseSpeed { get; private set; } = 1f;
    [field: SerializeField] public float RotationTime { get; private set; } = 0.14f;
    [field: SerializeField] public PlayerDashData PlayerDashData { get; private set; }
    [field: SerializeField] public PlayerStopData PlayerStopData { get; private set; }
    [field: SerializeField] public PlayerRunData PlayerRunData { get; private set; }
    [field: SerializeField] public PlayerSprintData PlayerSprintData { get; private set; }
}
