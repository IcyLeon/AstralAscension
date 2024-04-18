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
        StartAnimation("isESkill");
        playableCharacterStateMachine.playableCharacters.OnDamageHit += OnDamageHit;
    }

    protected virtual void OnDamageHit(object source)
    {

    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation("isESkill");
        playableCharacterStateMachine.playableCharacters.OnDamageHit -= OnDamageHit;
    }
}