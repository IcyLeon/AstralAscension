using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGroundedState : PlayerMovementState
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
        playerController.playerInputAction.Jump.started += Jump_started;
        playerController.playerInputAction.Dash.started += Dash_started;
        playerController.playerInputAction.Attack.performed += Attack_performed;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        playerController.playerInputAction.Jump.started -= Jump_started;
        playerController.playerInputAction.Dash.started -= Dash_started;
        playerController.playerInputAction.Attack.performed -= Attack_performed;
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Attack();
    }

    private void Attack()
    {
        if (IsSkillCasting() || playableCharacterStateMachine.IsAttacking())
            return;

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerCharacterAttackState);
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

        if (playableCharacterStateMachine.player.playerData.canSprint)
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
