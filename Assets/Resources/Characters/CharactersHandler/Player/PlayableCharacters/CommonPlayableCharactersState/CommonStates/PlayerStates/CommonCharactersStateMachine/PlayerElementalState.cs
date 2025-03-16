using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerElementalState : IState, IPlayableElementalState
{
    protected SkillStateMachine skill { get; }

    public PlayerElementalState(SkillStateMachine skill)
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
    }

    public virtual void OnDisable()
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
        playableCharacterStateMachine.playerStateMachine.player.playerData.UpdateTargetRotationData(data);
    }

    public void SmoothRotateToTargetRotation()
    {
        playableCharacterStateMachine.playerStateMachine.player.playerData.SmoothRotateToTargetRotation();
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
