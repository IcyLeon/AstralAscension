using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    private StateMachineManager<PlayerMovementState> StateMachineManager;
    public PlayableCharacters playableCharacter { get; }
    public PlayerIdleState playerIdleState { get; }
    public PlayerRunState playerRunState { get; }
    public PlayerWeakStopState playerWeakStopState { get; }
    public PlayerDashState playerDashState { get; }
    public PlayerStrongStopState playerStrongStopState { get; }
    public PlayerJumpState playerJumpState { get; }
    public PlayerFallingState playerFallingState { get; }
    public PlayerSprintState playerSprintState { get; }
    public PlayerHardLandingState playerHardLandingState { get; }
    public PlayerSoftLandingState playerSoftLandingState { get; }
    public PlayerPlungeLandingState playerPlungeLandingState { get; }
    public PlayerPlungeState playerPlungeState { get; }
    public PlayerAimState playerAimState { get; }

    public Player player
    {
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

    public void Update()
    {
        StateMachineManager.Update();
    }
    public void FixedUpdate()
    {
        StateMachineManager.FixedUpdate();
    }

    public void LateUpdate()
    {
        StateMachineManager.LateUpdate();
    }

    public void OnAnimationTransition()
    {
        StateMachineManager.OnAnimationTransition();
    }

    public void OnCollisionEnter(Collision collision)
    {
        StateMachineManager.OnCollisionEnter(collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        StateMachineManager.OnCollisionExit(collision);
    }

    public void OnCollisionStay(Collision collision)
    {
        StateMachineManager.OnCollisionStay(collision);
    }
    public void OnTriggerEnter(Collider Collider)
    {
        StateMachineManager.OnTriggerEnter(Collider);
    }

    public void OnTriggerExit(Collider Collider)
    {
        StateMachineManager.OnTriggerExit(Collider);
    }

    public void OnTriggerStay(Collider Collider)
    {
        StateMachineManager.OnTriggerStay(Collider);
    }


    public void ChangeState(IState newState)
    {
        StateMachineManager.ChangeState((PlayerMovementState)newState);
    }

    public PlayerStateMachine(PlayableCharacterStateMachine PCS)
    {
        StateMachineManager = new StateMachineManager<PlayerMovementState>();
        playableCharacter = PCS.playableCharacters;
        playerAimState = new PlayerAimState(this);
        playerIdleState = new PlayerIdleState(this);
        playerRunState = new PlayerRunState(this);
        playerWeakStopState = new PlayerWeakStopState(this);
        playerJumpState = new PlayerJumpState(this);
        playerDashState = new PlayerDashState(this);
        playerStrongStopState = new PlayerStrongStopState(this);
        playerFallingState = new PlayerFallingState(this);
        playerSoftLandingState = new PlayerSoftLandingState(this);
        playerHardLandingState = new PlayerHardLandingState(this);
        playerPlungeLandingState = new PlayerPlungeLandingState(this);
        playerSprintState = new PlayerSprintState(this);
        playerPlungeState = new PlayerPlungeState(this);
        ChangeState(playerIdleState);
    }
}
