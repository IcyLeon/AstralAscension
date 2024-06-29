using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerCharacterState : DamageableEntityState
{
    public PlayerCharacterState(CharacterStateMachine CharacterStateMachine) : base(CharacterStateMachine)
    {
    }

    protected PlayableCharacterStateMachine playableCharacterStateMachine
    {
        get
        {
            return characterStateMachine as PlayableCharacterStateMachine;
        }
    }

    protected override void OnDamageHit(float BaseDamageAmount)
    {
        if (playableCharacterStateMachine.playerStateMachine.IsInState<PlayerAirborneState>())
            return;

        base.OnDamageHit(BaseDamageAmount);
    }

    protected override void Attack()
    {
        if (playableCharacterStateMachine.IsAttacking())
            return;

        if (!playableCharacterStateMachine.playerStateMachine.IsInState<PlayerGroundedState>() ||
            playableCharacterStateMachine.playerStateMachine.IsInState<PlayerDashState>())
            return;

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerCharacterAttackState);
    }

    public override void UpdateTargetRotationData(float angle)
    {
        playableCharacterStateMachine.playerStateMachine.UpdateTargetRotationData(angle);
    }
    public override void SmoothRotateToTargetRotation()
    {
        playableCharacterStateMachine.playerStateMachine.SmoothRotateToTargetRotation();
    }

    public override void Enter()
    {
        base.Enter();

        if (playableCharacterStateMachine.playerElementalBurst != null)
            playableCharacterStateMachine.playerElementalBurst.elementalBurstControlBaseState.Enter();

        if (playableCharacterStateMachine.playerElementalSkill != null)
            playableCharacterStateMachine.playerElementalSkill.elementalSkillControlBaseState.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        if (playableCharacterStateMachine.playerElementalBurst != null)
            playableCharacterStateMachine.playerElementalBurst.elementalBurstControlBaseState.Exit();

        if (playableCharacterStateMachine.playerElementalSkill != null)
            playableCharacterStateMachine.playerElementalSkill.elementalSkillControlBaseState.Exit();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        playableCharacterStateMachine.player.PlayerController.playerInputAction.Attack.started += Attack_performed;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        playableCharacterStateMachine.player.PlayerController.playerInputAction.Attack.started -= Attack_performed;
    }

    protected virtual void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Attack();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();


        if (playableCharacterStateMachine.playerElementalBurst != null)
            playableCharacterStateMachine.playerElementalBurst.elementalBurstControlBaseState.FixedUpdate();

        if (playableCharacterStateMachine.playerElementalSkill != null)
            playableCharacterStateMachine.playerElementalSkill.elementalSkillControlBaseState.FixedUpdate();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();


        if (playableCharacterStateMachine.playerElementalBurst != null)
            playableCharacterStateMachine.playerElementalBurst.elementalBurstControlBaseState.LateUpdate();

        if (playableCharacterStateMachine.playerElementalSkill != null)
            playableCharacterStateMachine.playerElementalSkill.elementalSkillControlBaseState.LateUpdate();

    }

    public override void Update()
    {
        base.Update();

        if (playableCharacterStateMachine.playerElementalBurst != null)
            playableCharacterStateMachine.playerElementalBurst.elementalBurstControlBaseState.Update();

        if (playableCharacterStateMachine.playerElementalSkill != null)
            playableCharacterStateMachine.playerElementalSkill.elementalSkillControlBaseState.Update();

        OnPlungeUpdate();
    }

    protected virtual void OnPlungeUpdate()
    {
        if (playableCharacterStateMachine.playerStateMachine == null)
            return;
         
        if (playableCharacterStateMachine.playerStateMachine.IsInState<PlayerPlungeState>())
        {
            playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playableCharacterPlungeAttackState);
            return;
        }
    }

    public override void OnAnimationTransition()
    {
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    public override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
    }

    public override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
    }

    public override void OnTriggerEnter(Collider Collider)
    {
        base.OnTriggerEnter(Collider);

    }

    public override void OnTriggerExit(Collider Collider)
    {
        base.OnTriggerExit(Collider);

    }

    public override void OnTriggerStay(Collider Collider)
    {
        base.OnTriggerStay(Collider);
    }
}
