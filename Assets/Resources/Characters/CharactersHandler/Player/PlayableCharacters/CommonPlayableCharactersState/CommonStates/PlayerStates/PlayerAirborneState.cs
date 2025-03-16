using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerMovementState
{
    public PlayerAirborneState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.groundParameter);
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
        if (!CheckGroundDistance(playableCharacterStateMachine.player.playerData.airborneData.PlayerPlungeData.GroundCheckDistance))
            return;

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerPlungeState);
    }

    protected void LimitFallVelocity()
    {
        float FallSpeedLimit = playableCharacterStateMachine.player.playerData.airborneData.PlayerFallData.FallLimitVelocity;
        Vector3 velocity = GetVerticalVelocity();
        float limitVelocityY = Mathf.Max(velocity.y, -FallSpeedLimit);
        playableCharacterStateMachine.player.Rb.velocity = new Vector3(playableCharacterStateMachine.player.Rb.velocity.x, limitVelocityY, playableCharacterStateMachine.player.Rb.velocity.z);
    }

    protected void OnGroundTransition()
    {
        if (IsGrounded())
        {
            if (Mathf.Abs(GetVerticalVelocity().y) < 10f) // got issue
            {
                playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerSoftLandingState);
                return;
            }
            playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerHardLandingState);
            return;
        }
    }

    protected bool CheckGroundDistance(float distance)
    {
        Vector3 position = playableCharacterStateMachine.player.Rb.position;
        position.y += playableCharacterStateMachine.playableCharacter.MainCollider.radius / 2f;

        Vector3 bottom = position + Vector3.down * distance;

        return !Physics.CheckCapsule(position, bottom, playableCharacterStateMachine.playableCharacter.MainCollider.radius, ~LayerMask.GetMask("Player", "Ignore Raycast"), QueryTriggerInteraction.Ignore);
    }
}
