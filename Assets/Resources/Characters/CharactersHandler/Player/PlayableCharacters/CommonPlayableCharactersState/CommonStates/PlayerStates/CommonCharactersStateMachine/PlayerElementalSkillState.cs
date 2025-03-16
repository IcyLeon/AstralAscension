using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerElementalSkillState : PlayerElementalState
{
    public PlayerElementalSkillState(SkillStateMachine Skill) : base(Skill)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.elementalStateHash.elementalSkillParameter);
        playableCharacterStateMachine.playableCharacter.OnTakeDamage += OnDamageHit;
    }

    protected virtual void OnDamageHit(float BaseDamageAmount)
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerIdleState);
    }

    public override void Exit()
    {
        base.Exit();
        playableCharacterStateMachine.playableCharacter.OnTakeDamage -= OnDamageHit;
    }
}