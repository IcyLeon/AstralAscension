using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterAttackData
{
    private const float attackCooldown = 0.15f;
    private float attackCooldownElapsedTime;

    public PlayableCharacterAttackData()
    {
        OnEnable();
    }

    public void OnEnable()
    {
        attackCooldownElapsedTime = 0f;
    }

    public void OnDisable()
    {

    }

    public void Update()
    {
        UpdateAttackCooldown();
    }

    public void ResetAttackCooldown()
    {
        attackCooldownElapsedTime = attackCooldown;
    }

    public bool CanAttack()
    {
        return attackCooldownElapsedTime <= 0f;
    }

    private void UpdateAttackCooldown()
    {
        attackCooldownElapsedTime = Mathf.Clamp(attackCooldownElapsedTime - Time.deltaTime, 0f, attackCooldown);
    }
}
