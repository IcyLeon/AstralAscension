using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterAttackState : IState
{
    protected PlayableCharacterAttackStateMachine playableCharacterAttackStateMachine { get; }
    protected PlayableCharacterStateMachine playableCharacterStateMachine;
    public PlayableCharacterAttackState(PlayableCharacterAttackStateMachine PlayableCharacterAttackStateMachine)
    {
        playableCharacterAttackStateMachine = PlayableCharacterAttackStateMachine;
        playableCharacterStateMachine = playableCharacterAttackStateMachine.playableCharacterStateMachine;
    }

    public virtual void Enter()
    {
        OnEnable();
        StartAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackParameter);
        playableCharacterStateMachine.ResetVelocity();
    }

    public virtual void Exit()
    {
        OnDisable();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackParameter);
    }


    public virtual void OnDisable()
    {
    }

    public virtual void OnEnable()
    {
    }

    protected virtual void OnAttackInput()
    {

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

    public void SetAnimationTrigger(string parameter)
    {
        playableCharacterStateMachine.SetAnimationTrigger(parameter);
    }

    public void UpdateTargetRotationData(float angle)
    {
        playableCharacterStateMachine.playerData.UpdateTargetRotationData(angle);
    }

    public void StartAnimation(string parameter)
    {
        playableCharacterStateMachine.StartAnimation(parameter);
    }

    public void StopAnimation(string parameter)
    {
        playableCharacterStateMachine.StopAnimation(parameter);
    }


    public virtual void Update()
    {

    }
}
