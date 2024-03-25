using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerGroundedState
{
    public PlayerFallingState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        LimitFallVelocity();
    }

    private void LimitFallVelocity()
    {
        float FallSpeedLimit = playerStateMachine.playerData.airborneData.PlayerFallData.FallLimitVelocity;
        Vector3 velocity = GetVerticalVelocity();
        if (velocity.y >= -FallSpeedLimit)
        {
            return;
        }

        Vector3 limitVel = new Vector3(0f, -FallSpeedLimit - velocity.y, 0f);
        playerStateMachine.player.Rb.AddForce(limitVel, ForceMode.VelocityChange);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
