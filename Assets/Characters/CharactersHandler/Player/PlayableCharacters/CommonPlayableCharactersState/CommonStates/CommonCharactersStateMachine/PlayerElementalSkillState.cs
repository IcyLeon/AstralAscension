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
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation("isESkill");
    }
}