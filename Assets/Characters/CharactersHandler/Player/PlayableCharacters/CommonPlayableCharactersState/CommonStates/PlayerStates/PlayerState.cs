using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public PlayableCharacterState playableCharacterState { get; }
    public Player player { 
        get
        {
            return playableCharacterState.player;
        }
    }

    private IState currentStates;

    public void Update()
    {
        if (currentStates != null)
            currentStates.Update();
    }
    public void FixedUpdate()
    {
        if (currentStates != null)
            currentStates.FixedUpdate();
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

    public void ChangeState(IState newState)
    {
        if (currentStates != null)
            currentStates.Exit();

        currentStates = newState;

        currentStates.Enter();
    }

    public PlayerState(PlayableCharacterState PCS)
    {
        playableCharacterState = PCS;

    }
}
