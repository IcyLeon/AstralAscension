using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements
{
    private float activeTimer;
    public ElementsSO elementsSO { get; }

    public Elements(ElementsSO e)
    {
        elementsSO = e;
    }
}
