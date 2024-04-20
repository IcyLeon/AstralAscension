using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerMovementState
{
    public PlayerAirborneState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void SubscribeInputs()
    {
        base.SubscribeInputs();
        playerStateMachine.player.PlayerController.playerInputAction.Attack.performed += Attack_performed;
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CheckGroundDistance(playerStateMachine.playerData.airborneData.PlayerPlungeData.GroundCheckDistance))
            return;

        if (playerStateMachine.IsInState<PlayerPlungeState>())
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerPlungeState);
    }

    public override void UnsubscribeInputs()
    {
        base.UnsubscribeInputs();
        playerStateMachine.player.PlayerController.playerInputAction.Attack.performed -= Attack_performed;
    }

    protected void LimitFallVelocity()
    {
        float FallSpeedLimit = playerStateMachine.playerData.airborneData.PlayerFallData.FallLimitVelocity;
        Vector3 velocity = GetVerticalVelocity();
        float limitVelocityY = Mathf.Max(velocity.y, -FallSpeedLimit);
        playerStateMachine.player.Rb.velocity = new Vector3(playerStateMachine.player.Rb.velocity.x, limitVelocityY, playerStateMachine.player.Rb.velocity.z);
    }

    protected void OnGroundTransition()
    {
        if (IsGrounded())
        {
            if (Mathf.Abs(GetVerticalVelocity().y) < 10f) // got issue
            {
                playerStateMachine.ChangeState(playerStateMachine.playerSoftLandingState);
                return;
            }
            playerStateMachine.ChangeState(playerStateMachine.playerHardLandingState);
            return;
        }
    }

    public override void Enter()
    {
        base.Enter();
        StopAnimation("isGrounded");
    }

    protected bool CheckGroundDistance(float distance)
    {
        Vector3 position = playerStateMachine.player.Rb.position;
        position.y += playerStateMachine.playableCharacter.MainCollider.radius / 2f;

        Vector3 bottom = position + Vector3.down * distance;

        return !Physics.CheckCapsule(position, bottom, playerStateMachine.playableCharacter.MainCollider.radius, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);
    }
}
