using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerElementalSkillState : PlayerElementalState
{
    public PlayerElementalSkillState(Skill Skill) : base(Skill)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(playableCharacterStateMachine.playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.elementalStateHash.elementalSkillParameter);
        playableCharacterStateMachine.playableCharacters.OnTakeDamage += OnDamageHit;
    }

    protected virtual void OnDamageHit(float BaseDamageAmount)
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
    }

    public override void Exit()
    {
        base.Exit();
        playableCharacterStateMachine.playableCharacters.OnTakeDamage -= OnDamageHit;
    }
}