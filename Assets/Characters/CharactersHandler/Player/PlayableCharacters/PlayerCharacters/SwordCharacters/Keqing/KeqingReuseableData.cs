using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingReuseableData : SwordReuseableData
{
    public Vector3 targetPosition;

    public KeqingReuseableData() : base()
    {
        targetPosition = Vector3.zero;
    }
}
