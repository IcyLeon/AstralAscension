using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerElementalState : IState, IPlayableElementalState
{
    protected Skill skill { get; }

    public PlayerElementalState(Skill skill)
    {
        this.skill = skill;
    }

    protected PlayableCharacterStateMachine playableCharacterStateMachine
    {
        get
        {
            return skill.playableCharacterStateMachine;
        }
    }

    public virtual void Enter()
    {
        OnEnable();
        SkillBurstManager.AddState(this);
        StartAnimation(playableCharacterStateMachine.playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.elementalStateParameter);
        playableCharacterStateMachine.playerStateMachine.ResetVelocity();
    }

    public virtual void Exit()
    {
        OnDisable();
        StopAnimation(playableCharacterStateMachine.playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.elementalStateParameter);
    }

    /// <summary>
    /// Useful if a second input during the skill casting is required
    /// </summary>
    public virtual void OnEnable()
    {
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed += ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started += ElementalSkill_started;
    }

    public virtual void OnDisable()
    {
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed -= ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started -= ElementalSkill_started;
    }

    private void ElementalSkill_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ElementalSkill_started();
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ElementalSkill_canceled();
    }

    private void ElementalSkill_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ElementalSkill_performed();
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

    protected PlayableCharacters playableCharacters
    {
        get
        {
            return playableCharacterStateMachine.playableCharacters;
        }
    }
    public virtual void FixedUpdate()
    {
    }

    public virtual void LateUpdate()
    {
    }

    public virtual void OnAnimationTransition()
    {
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
    }

    public virtual void OnCollisionExit(Collision collision)
    {
    }

    public virtual void OnCollisionStay(Collision collision)
    {
    }

    public virtual void OnTriggerEnter(Collider Collider)
    {
    }

    public virtual void OnTriggerExit(Collider Collider)
    {
    }

    public virtual void OnTriggerStay(Collider Collider)
    {
    }

    public virtual void Update()
    {
    }

    public void StartAnimation(string parameter)
    {
        playableCharacterStateMachine.StartAnimation(parameter);
    }

    public void SetAnimationTrigger(string parameter)
    {
        playableCharacterStateMachine.SetAnimationTrigger(parameter);
    }
    public void StopAnimation(string parameter)
    {
        playableCharacterStateMachine.StopAnimation(parameter);
    }

    public void UpdateTargetRotationData(float data)
    {
        playableCharacterStateMachine.playerStateMachine.UpdateTargetRotationData(data);
    }

    public void SmoothRotateToTargetRotation()
    {
        playableCharacterStateMachine.playerStateMachine.SmoothRotateToTargetRotation();
    }

    public virtual bool IsElementalStateEnded()
    {
        return playableCharacters.IsDead();
    }

    public virtual void OnElementalStateEnter()
    {
    }

    public virtual void UpdateElementalState()
    {
    }

    public virtual void OnElementalStateExit()
    {

    }
}
