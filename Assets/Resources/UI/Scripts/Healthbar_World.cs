using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_World : Healthbar
{
    // Start is called before the first frame update
    void Awake()
    {
        damageable = GetComponentInParent<IDamageable>();
    }

    private void OnEnable()
    {
        damageable.OnTakeDamage += Damageable_OnTakeDamage;
    }

    private void OnDisable()
    {
        damageable.OnTakeDamage -= Damageable_OnTakeDamage;
    }
}
