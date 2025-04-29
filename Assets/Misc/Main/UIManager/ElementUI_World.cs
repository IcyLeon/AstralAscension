using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementUI_World : ElementUI
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        IDamageable damageable = GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            SetCharacterDataStat(damageable.GetCharacterDataStat());
        }
    }

    private void OnEnable()
    {
        if (characterDataStat == null)
            return;

        characterDataStat.OnElementEnter += OnElementEnter;
        characterDataStat.OnElementExit += OnElementExit;
    }

    private void OnDisable()
    {
        if (characterDataStat == null)
            return;

        characterDataStat.OnElementEnter -= OnElementEnter;
        characterDataStat.OnElementExit -= OnElementExit;
    }
}
