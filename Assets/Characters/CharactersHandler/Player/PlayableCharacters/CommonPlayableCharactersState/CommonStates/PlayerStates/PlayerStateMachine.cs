using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayableCharacters playableCharacter { get; }
    public PlayerIdleState playerIdleState { get; }
    public PlayerRunState playerRunState { get; }
    public PlayerWeakStopState playerWeakStopState { get; }
    public PlayerJumpState playerJumpState { get; }
    public PlayerFallingState playerFallingState { get; }

    public Player player { 
        get
        {
            return playableCharacter.player;
        }
    }

    public PlayerData playerData
    {
        get
        {
            return player.playerData;
        }
    }

    private IState currentStates;

    public void Update()
    {
        if (currentStates != null)
            currentStates.Update();

        Debug.Log(currentStates);
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

    public PlayerStateMachine(PlayableCharacterStateMachine PCS)
    {
        playableCharacter = PCS.playableCharacters;
        playerIdleState = new PlayerIdleState(this);
        playerRunState = new PlayerRunState(this);
        playerWeakStopState = new PlayerWeakStopState(this);
        playerJumpState = new PlayerJumpState(this);
        playerFallingState = new PlayerFallingState(this);
        ChangeState(playerIdleState);
    }
}
