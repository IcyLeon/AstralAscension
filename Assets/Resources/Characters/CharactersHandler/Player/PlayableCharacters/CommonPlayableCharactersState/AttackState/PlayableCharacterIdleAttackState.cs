using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterIdleAttackState : PlayableCharacterAttackState
{
    private float attacklastTimeElasped;
    private const float basicAttackIdleTime = 3f;
    private PlayerIdleState playerIdleState;
    public PlayableCharacterIdleAttackState(PlayableCharacterAttackStateMachine PlayableCharacterAttackStateMachine) : base(PlayableCharacterAttackStateMachine)
    {
        playerIdleState = playableCharacterStateMachine.playerIdleState;
    }

    public override void Enter()
    {
        base.Enter();
        playableCharacterAttackStateMachine.ResetAttackState();
        ResetAttackElasped();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playerIdleState.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        playerIdleState.OnDisable();
    }

    public override void Update()
    {
        base.Update();

        if (Time.time - attacklastTimeElasped >= basicAttackIdleTime)
        {
            playableCharacterStateMachine.ChangeState(playerIdleState);
            return;
        }

        playerIdleState.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        playerIdleState.FixedUpdate();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        playerIdleState.LateUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        ResetAttackElasped();
    }

    private void ResetAttackElasped()
    {
        attacklastTimeElasped = Time.time;
    }
}
