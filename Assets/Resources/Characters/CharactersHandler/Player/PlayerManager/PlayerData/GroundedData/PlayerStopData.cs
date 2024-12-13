using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStopData
{
    [field: SerializeField] public float WeakDecelerationForce { get; private set; } = 5f;
    [field: SerializeField] public float StrongDecelerationForce { get; private set; } = 6.5f;
}
