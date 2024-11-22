using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerMovementState
{
    public PlayerAirborneState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.groundParameter);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playerController.playerInputAction.Attack.performed += Attack_performed;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        playerController.playerInputAction.Attack.performed -= Attack_performed;
    }


    protected virtual void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CheckGroundDistance(playerStateMachine.playerData.airborneData.PlayerPlungeData.GroundCheckDistance))
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerPlungeState);
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

    protected bool CheckGroundDistance(float distance)
    {
        Vector3 position = playerStateMachine.player.Rb.position;
        position.y += playerStateMachine.playableCharacter.MainCollider.radius / 2f;

        Vector3 bottom = position + Vector3.down * distance;

        return !Physics.CheckCapsule(position, bottom, playerStateMachine.playableCharacter.MainCollider.radius, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);
    }
}
