using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.groundParameter);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playableCharacterStateMachine.player.playerController.playerInputAction.Jump.started += Jump_started;
        playableCharacterStateMachine.player.playerController.playerInputAction.Dash.started += Dash_started;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        playableCharacterStateMachine.player.playerController.playerInputAction.Jump.started -= Jump_started;
        playableCharacterStateMachine.player.playerController.playerInputAction.Dash.started -= Dash_started;
    }

    private void Dash_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (IsSkillCasting())
            return;

        Dash_started();
    }

    private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (IsSkillCasting())
            return;

        Jump_started();
    }

    protected virtual void Dash_started()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerDashState);
    }

    protected virtual void Jump_started()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerJumpState);
    }

    public override void Update()
    {
        base.Update();

        if (!IsGrounded())
        {
            OnFall();
            return;
        }
    }


    protected void OnMove()
    {
        if (IsSkillCasting())
            return;

        if (playableCharacterStateMachine.playerData.canSprint)
        {
            playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerSprintState);
            return;
        }

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerRunState);
    }

    private void OnFall()
    {
        if (IsSkillCasting())
            return;

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerFallingState);
    }
}
