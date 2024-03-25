using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDashData
{
    [field: SerializeField] public float RotationTime { get; private set; } = 1f;
    [field: SerializeField] public int ConsecutiveDashesLimitAmount { get; private set; } = 2;
    [field: SerializeField] public float TimeToBeConsideredConsecutive { get; private set; } = 1f;
    [field: SerializeField] public float DashLimitReachedCooldown { get; private set; } = 1.75f;
}
