using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAttackData
{
    [field: SerializeField] public float AttackCooldown { get; private set; }
}
