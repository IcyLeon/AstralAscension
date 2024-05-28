using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class PlayableCharacterReuseableData : CharacterReuseableData
{
    private float inputCounterElapsed;
    private const float inputCounterTime = 0.45f;

    private float basicAttackIdleElapsed;
    private const float basicAttackIdleTime = 3f;

    private float basicAttackCooldownElapsed;
    private float basicAttackCooldown;
    private int basicAttackPhase;
    private int totalAttackPhase;
    private bool canAttack;

    public PlayableCharacterReuseableData(int TotalAttackPhase, CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
        ResetData();
        totalAttackPhase = TotalAttackPhase;
        basicAttackCooldown = playableCharacterStateMachine.player.PlayerSO.GroundedData.PlayerAttackData.AttackCooldown;
    }

    protected PlayableCharacterStateMachine playableCharacterStateMachine
    {
        get
        {
            return (PlayableCharacterStateMachine)characterStateMachine;
        }
    }

    public void ResetAttack()
    {
        basicAttackPhase = 0;
        basicAttackCooldownElapsed = 0;
    }

    public void DoBasicAttack()
    {
        if (!canAttack)
            return;

        if (Time.time - inputCounterElapsed > inputCounterTime)
        {
            basicAttackPhase++;
            basicAttackPhase = Mathf.Clamp(basicAttackPhase, 0, totalAttackPhase);
            characterStateMachine.SetAnimationTrigger(basicAttackPhase.ToString());
            inputCounterElapsed = Time.time;
        }
    }

    private void UpdateAttack()
    {
        if (basicAttackPhase >= totalAttackPhase && canAttack)
        {
            basicAttackCooldownElapsed = basicAttackCooldown;
            canAttack = false;
            return;
        }

        if (!canAttack)
        {
            if (basicAttackCooldownElapsed <= 0)
            {
                ResetAttack();
                canAttack = true;
                return;
            }
            basicAttackCooldownElapsed -= Time.deltaTime;
        }
    }

    public void UpdateAttackIdleState()
    {
        basicAttackIdleElapsed = Time.time;
    }

    public bool CanTransitBackToIdleState()
    {
        return Time.time - basicAttackIdleElapsed > basicAttackIdleTime;
    }

    public override void ResetData()
    {
        base.ResetData();

        canAttack = true;
        ResetAttack();
        inputCounterElapsed = Time.time;
    }

    public override void Update()
    {
        base.Update();

        UpdateAttack();
    }
}
