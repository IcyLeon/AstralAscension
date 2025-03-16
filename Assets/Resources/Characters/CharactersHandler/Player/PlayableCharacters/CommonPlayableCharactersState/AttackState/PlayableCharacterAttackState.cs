using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterAttackState : IState
{
    protected float duration;
    private float elapseTime;

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
        elapseTime = 0;
    }

    public virtual void Exit()
    {
        OnDisable();
    }


    public virtual void OnDisable()
    {
        playableCharacterStateMachine.playerController.playerInputAction.Attack.performed -= Attack_performed;
    }

    public virtual void OnEnable()
    {
        playableCharacterStateMachine.playerController.playerInputAction.Attack.performed += Attack_performed;
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAttackInput();
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

    public virtual void SetAnimationTrigger(string parameter)
    {
    }

    public virtual void SmoothRotateToTargetRotation()
    {
    }

    public virtual void StartAnimation(string parameter)
    {
    }

    public virtual void StopAnimation(string parameter)
    {
    }

    public virtual void Update()
    {
        UpdateAttackElapsed();

    }

    private void UpdateAttackElapsed()
    {
        if (elapseTime > duration)
        {
            OnElapsedTimeEnds();
            return;
        }
        elapseTime += Time.deltaTime;
    }

    protected virtual void OnElapsedTimeEnds()
    {

    }

    public virtual void UpdateTargetRotationData(float angle)
    {
    }
}
