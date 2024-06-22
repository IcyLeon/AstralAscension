using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_World : Healthbar
{
    // Start is called before the first frame update
    void Awake()
    {
        IDamageable damageable = GetComponentInParent<IDamageable>();

        if (damageable != null)
            SetCharacterDataStat(damageable.GetCharacterDataStat());
    }
}
