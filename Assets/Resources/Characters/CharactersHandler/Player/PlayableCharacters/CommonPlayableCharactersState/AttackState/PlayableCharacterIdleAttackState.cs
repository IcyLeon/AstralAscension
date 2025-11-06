using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterIdleAttackState : PlayerIdleState
{
    private float attacklastTimeElasped;
    private const float basicAttackIdleTime = 3f;

    public PlayableCharacterIdleAttackState(PlayableCharacterStateMachine PS) : base(PS)
    {
        attacklastTimeElasped = Time.time;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackParameter);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackParameter);
    }

    public override void Update()
    {
        base.Update();
        UpdateIdleState();
    }

    private void UpdateIdleState()
    {
        if (Time.time - attacklastTimeElasped <= basicAttackIdleTime)
            return;

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerIdleState);
    }
}
