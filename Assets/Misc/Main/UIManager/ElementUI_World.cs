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
        if (GetCharacterDataStat() == null)
            return;

        GetCharacterDataStat().OnElementEnter += OnElementEnter;
        GetCharacterDataStat().OnElementExit += OnElementExit;
    }

    private void OnDisable()
    {
        if (GetCharacterDataStat() == null)
            return;

        GetCharacterDataStat().OnElementEnter -= OnElementEnter;
        GetCharacterDataStat().OnElementExit -= OnElementExit;
    }
}
