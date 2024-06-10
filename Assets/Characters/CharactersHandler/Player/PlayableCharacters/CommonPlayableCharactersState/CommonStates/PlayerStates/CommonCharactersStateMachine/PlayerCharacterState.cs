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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        playableCharacterStateMachine.player.PlayerController.playerInputAction.Attack.started += Attack_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed += ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started += ElementalSkill_started;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalBurst.performed += ElementalBurst_performed;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        playableCharacterStateMachine.player.PlayerController.playerInputAction.Attack.started -= Attack_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed -= ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started -= ElementalSkill_started;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalBurst.performed -= ElementalBurst_performed;
    }

    protected virtual bool CanTransitToAnyElementalState()
    {
        return playableCharacterStateMachine.playerStateMachine.IsInState<PlayerGroundedState>();
    }

    private bool CanTransitToElementalSkillState()
    {
        return CanTransitToAnyElementalState() && playableCharacterStateMachine.playableCharacters.playableCharacterDataStat.CanUseElementalSkill() &&
            !playableCharacterStateMachine.IsSkillCasting();
    }
    private bool CanTransitToElementalBurstState()
    {
        return CanTransitToAnyElementalState() && playableCharacterStateMachine.playableCharacters.playableCharacterDataStat.CanUseElementalBurst() &&
            !playableCharacterStateMachine.IsSkillCasting();
    }

    protected virtual void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Attack();
    }

    private void ElementalSkill_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalSkillState())
            return;

        ElementalSkill_started();
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalSkillState())
            return;

        ElementalSkill_canceled();
    }

    private void ElementalSkill_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalSkillState())
            return;

        ElementalSkill_performed();
    }

    private void ElementalBurst_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalBurstState())
            return;

        ElementalBurst_performed();
    }

    protected virtual void ElementalSkill_started()
    {
    }
    protected virtual void ElementalSkill_canceled()
    {
    }

    protected virtual void ElementalSkill_performed()
    {

    }

    protected virtual void ElementalBurst_performed()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerElementalBurstState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void Update()
    {
        base.Update();

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
