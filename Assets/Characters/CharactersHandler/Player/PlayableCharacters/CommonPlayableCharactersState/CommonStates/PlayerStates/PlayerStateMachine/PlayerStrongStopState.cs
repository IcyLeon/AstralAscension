using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrongStopState : PlayerStopState
{
    public PlayerStrongStopState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerStateMachine.playerData.DecelerateForce = playerStateMachine.playerData.groundedData.PlayerStopData.StrongDecelerationForce;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
