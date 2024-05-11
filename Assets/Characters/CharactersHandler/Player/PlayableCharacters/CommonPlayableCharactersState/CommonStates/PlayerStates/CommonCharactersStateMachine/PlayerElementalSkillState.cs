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
        playableCharacterStateMachine.playableCharacters.OnDamageHit += OnDamageHit;
    }

    protected virtual void OnDamageHit(object source)
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playableCharacterStateMachine.playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.elementalStateParameter);
        playableCharacterStateMachine.playableCharacters.OnDamageHit -= OnDamageHit;
    }
}