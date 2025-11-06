using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterAttackState : PlayerGroundedState
{
    private float attackDelayTimeElapsed;

    protected PlayableCharacterAttackStateMachine playableCharacterAttackStateMachine { get; }

    public PlayableCharacterAttackState(PlayableCharacterAttackStateMachine PlayableCharacterAttackStateMachine, PlayableCharacterStateMachine PS) : base(PS)
    {
        playableCharacterAttackStateMachine = PlayableCharacterAttackStateMachine;
        attackDelayTimeElapsed = Time.time;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackParameter);
        playableCharacterStateMachine.ResetVelocity();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playableCharacterStateMachine.player.playerController.playerInputAction.Attack.performed += Attack_performed;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        playableCharacterStateMachine.player.playerController.playerInputAction.Attack.performed -= Attack_performed;
    }

    protected override void Jump_started()
    {

    }

    protected virtual void TransitNextAttack()
    {

    }

    protected abstract float AttackDelayInterval();

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Time.time - attackDelayTimeElapsed >= AttackDelayInterval())
        {
            TransitNextAttack();
        }
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();
        playableCharacterStateMachine.ChangeState(new PlayableCharacterIdleAttackState(playableCharacterStateMachine));
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackParameter);
    }
}
