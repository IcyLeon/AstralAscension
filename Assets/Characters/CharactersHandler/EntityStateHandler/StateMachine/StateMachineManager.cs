using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager
{
    public IState currentStates { get; private set; }

    public void Update()
    {
        //Debug.Log(currentStates);
        if (currentStates != null)
            currentStates.Update();
    }
    public void FixedUpdate()
    {
        if (currentStates != null)
            currentStates.FixedUpdate();
    }

    public void OnEnable()
    {
        if (currentStates != null)
            currentStates.OnEnable();
    }
    public void OnDisable()
    {
        if (currentStates != null)
            currentStates.OnDisable();
    }

    public void LateUpdate()
    {
        if (currentStates != null)
            currentStates.LateUpdate();
    }

    public void OnAnimationTransition()
    {
        if (currentStates != null)
            currentStates.OnAnimationTransition();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (currentStates != null)
            currentStates.OnCollisionEnter(collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        if (currentStates != null)
            currentStates.OnCollisionExit(collision);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (currentStates != null)
            currentStates.OnCollisionStay(collision);
    }
    public void OnTriggerEnter(Collider Collider)
    {
        if (currentStates != null)
            currentStates.OnTriggerEnter(Collider);
    }

    public void OnTriggerExit(Collider Collider)
    {
        if (currentStates != null)
            currentStates.OnTriggerExit(Collider);
    }

    public void OnTriggerStay(Collider Collider)
    {
        if (currentStates != null)
            currentStates.OnTriggerStay(Collider);
    }

    public bool IsInState<T>()
    {
        return currentStates is T;
    }
    public void ChangeState(IState newState)
    {
        if (currentStates != null)
            currentStates.Exit();

        currentStates = newState;

        currentStates.Enter();
    }
}
