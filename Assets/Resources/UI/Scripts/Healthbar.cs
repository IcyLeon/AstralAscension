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
        if (damageable == null)
            return;

        healthBarSlider.value = damageable.GetCurrentHealth() / damageable.GetMaxHealth();
    }

    private void Update()
    {
        UpdateHealth();
    }
}
