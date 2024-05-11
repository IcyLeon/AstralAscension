using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntitySO : CharactersSO
{
    [field: SerializeField] public ElementsSO ElementSO { get; private set; }
}
