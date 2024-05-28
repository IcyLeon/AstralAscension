using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    protected IDamageable damageable;

    protected void UpdateHealth()
    {
        healthBarSlider.value = damageable.GetCurrentHealth() / damageable.GetMaxHealth();
    }

    protected void Damageable_OnTakeDamage(float DamageAmount)
    {
        if (damageable == null)
            return;

        UpdateHealth();
    }
}
