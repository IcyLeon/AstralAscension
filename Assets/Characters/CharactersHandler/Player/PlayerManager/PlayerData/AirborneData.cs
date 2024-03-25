using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AirborneData
{
    [field: SerializeField] public PlayerJumpData PlayerJumpData { get; private set; }
    [field: SerializeField] public PlayerFallData PlayerFallData { get; private set; }
}
