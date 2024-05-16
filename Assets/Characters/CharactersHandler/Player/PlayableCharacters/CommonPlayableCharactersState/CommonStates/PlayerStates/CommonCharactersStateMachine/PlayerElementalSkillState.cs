using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementalSkillState : PlayerElementalState
{
    public PlayerElementalSkillState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playableCharacterStateMachine.playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.elementalStateParameter);
        playableCharacterStateMachine.playableCharacters.OnTakeDamage += OnDamageHit;
    }

    protected virtual void OnDamageHit(IAttacker source, ElementsSO elementsSO, float BaseDamageAmount)
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playableCharacterStateMachine.playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.elementalStateParameter);
        playableCharacterStateMachine.playableCharacters.OnTakeDamage -= OnDamageHit;
    }
}