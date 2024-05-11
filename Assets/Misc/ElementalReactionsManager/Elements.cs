using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements
{
    public IDamageable target { get; }
    private const float TimeToDestroy = 8f;
    private float activeTimer;
    public bool canBeDestroyed { get; private set; }
    public ElementsSO elementsSO { get; }

    public delegate void OnElement(Elements e);
    public OnElement OnElementDestroy;

    public Elements(ElementsSO e, IDamageable target, bool isFixed = false)
    {
        elementsSO = e;
        canBeDestroyed = !isFixed;
        this.target = target;
        Reset();
    }

    public void Reset()
    {
        activeTimer = Time.time;
    }

    public void OnDestroy()
    {
        OnElementDestroy?.Invoke(this);
    }

    public void Update()
    {
        if (Time.time - activeTimer > TimeToDestroy && canBeDestroyed)
        {
            OnDestroy();
            return;
        }
    }

}
