using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager
{
    private IState currentState;
    private IState startState;
    public void Update()
    {
        Debug.Log(currentState);
        if (currentState != null)
            currentState.Update();
    }
    public void FixedUpdate()
    {
        if (currentState != null)
            currentState.FixedUpdate();
    }

    public void OnEnable()
    {
        if (currentState != null)
            currentState.OnEnable();
    }
    public void OnDisable()
    {
        if (currentState != null)
            currentState.OnDisable();
    }

    public void LateUpdate()
    {
        if (currentState != null)
            currentState.LateUpdate();
    }

    public void OnAnimationTransition()
    {
        if (currentState != null)
            currentState.OnAnimationTransition();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (currentState != null)
            currentState.OnCollisionEnter(collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        if (currentState != null)
            currentState.OnCollisionExit(collision);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (currentState != null)
            currentState.OnCollisionStay(collision);
    }
    public void OnTriggerEnter(Collider Collider)
    {
        if (currentState != null)
            currentState.OnTriggerEnter(Collider);
    }

    public void OnTriggerExit(Collider Collider)
    {
        if (currentState != null)
            currentState.OnTriggerExit(Collider);
    }

    public void OnTriggerStay(Collider Collider)
    {
        if (currentState != null)
            currentState.OnTriggerStay(Collider);
    }

    public bool IsInState<T>()
    {
        return currentState is T;
    }
    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;

        if (currentState != null)
            currentState.Enter();
    }

    public void StartState(IState newState)
    {
        if (currentState != null)
            return;

        ChangeState(newState);

        startState = currentState;
    }

    public void ResetState()
    {
        if (startState == null)
            return;

        ChangeState(startState);
    }
}
