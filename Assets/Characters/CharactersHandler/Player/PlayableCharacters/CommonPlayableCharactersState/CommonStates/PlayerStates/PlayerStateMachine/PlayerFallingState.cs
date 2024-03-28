using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirborneState
{
    public PlayerFallingState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerStateMachine.playerData.SpeedModifier = 0f;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        LimitFallVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (IsGrounded())
        {
            playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
            return;
        }
    }

    private void LimitFallVelocity()
    {
        float FallSpeedLimit = playerStateMachine.playerData.airborneData.PlayerFallData.FallLimitVelocity;
        Vector3 velocity = GetVerticalVelocity();
        if (velocity.y >= -FallSpeedLimit)
        {
            return;
        }

        float limitVelocityY = Mathf.Max(velocity.y, -FallSpeedLimit);
        playerStateMachine.player.Rb.velocity = new Vector3(playerStateMachine.player.Rb.velocity.x, limitVelocityY, playerStateMachine.player.Rb.velocity.z);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
