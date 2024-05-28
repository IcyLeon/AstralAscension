using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementsInfoSO : ScriptableObject
{
    [field: SerializeField] public Color ColorText { get; private set; }
}
