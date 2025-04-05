using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterAttackData
{
    private const float attackCooldown = 1.5f;
    private float attackCooldownElapsedTime;

    public PlayableCharacterAttackData()
    {
        attackCooldownElapsedTime = 0f;
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
        attackCooldownElapsedTime -= Time.deltaTime;
        attackCooldownElapsedTime = Mathf.Clamp(attackCooldownElapsedTime, 0f, attackCooldown);
    }
}
