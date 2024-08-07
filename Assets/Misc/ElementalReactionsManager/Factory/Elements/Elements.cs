using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements
{
    public CharacterDataStat target { get; }
    private const float TimeToDestroy = 8f;
    private float activeTimer;
    public bool canBeDestroyed { get; private set; }
    public ElementsSO elementsSO { get; }

    public delegate void OnElementEvent(Elements e);
    public event OnElementEvent OnElementDestroy;

    public Elements(ElementsSO e, CharacterDataStat characterDataStat, bool isFixed = false)
    {
        elementsSO = e;
        canBeDestroyed = !isFixed;
        target = characterDataStat;

        if (target != null)
        {
            target.AddElement(elementsSO, this);
        }

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

    public void DestroyEvents()
    {
        OnElementDestroy = null;
    }

    public void Update()
    {
        if (target == null || target.IsDead())
        {
            OnDestroy();
            return;
        }

        if (Time.time - activeTimer > TimeToDestroy && canBeDestroyed)
        {
            OnDestroy();
            return;
        }
    }

}
