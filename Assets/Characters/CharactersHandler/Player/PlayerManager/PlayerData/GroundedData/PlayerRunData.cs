using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerRunData
{
    [field: SerializeField] public float SpeedModifier { get; private set; } = 1f;
}
