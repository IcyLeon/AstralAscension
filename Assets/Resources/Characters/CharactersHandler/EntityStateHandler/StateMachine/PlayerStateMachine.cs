using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    private StateMachineManager StateMachineManager;
    public PlayableCharacterStateMachine PlayableCharacterStateMachine { get; }
    public PlayerIdleState playerIdleState { get; }
    public PlayerRunState playerRunState { get; }
    public PlayerWeakStopState playerWeakStopState { get; }
    public PlayerDashStopState playerDashStopState { get; }
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
    public PlayerAttackState playerAttackState { get; }
    public PlayerDeadState playerDeadState { get; }
    public PlayerController playerController { get; }
    public Player player
    {
        get
        {
            return playableCharacter.player;
        }
    }
    public PlayableCharacters playableCharacter
    {
        get
        {
            return PlayableCharacterStateMachine.playableCharacters;
        }
    }
    public void SmoothRotateToTargetRotation()
    {
        float currentAngleY = player.Rb.transform.eulerAngles.y;
        if (currentAngleY == playerData.targetYawRotation)
        {
            return;
        }

        float angle = Mathf.SmoothDampAngle(currentAngleY, playerData.targetYawRotation, ref playerData.dampedTargetRotationCurrentVelocity, playerData.rotationTime - playerData.dampedTargetRotationPassedTime);
        playerData.dampedTargetRotationPassedTime += Time.deltaTime;
        player.Rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));
    }

    public void ResetVelocity()
    {
        player.Rb.velocity = Vector3.zero;
    }
    public void UpdateTargetRotationData(float angle)
    {
        float currentAngle = playerData.targetYawRotation;

        if (currentAngle == angle)
            return;

        playerData.targetYawRotation = angle;
        playerData.dampedTargetRotationPassedTime = 0f;
    }

    public PlayerData playerData
    {
        get
        {
            return player.playerData;
        }
    }

    public bool IsInState<T>()
    {
        return StateMachineManager.IsInState<T>();
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
        StateMachineManager.ChangeState(newState);
    }

    private void StartState(IState newState)
    {
        StateMachineManager.StartState(newState);
    }

    public PlayerStateMachine(PlayableCharacterStateMachine PCS)
    {
        PlayableCharacterStateMachine = PCS;
        playerController = PlayerController.instance;
        StateMachineManager = new StateMachineManager();
        playerAimState = new PlayerAimState(this);
        playerIdleState = new PlayerIdleState(this);
        playerRunState = new PlayerRunState(this);
        playerWeakStopState = new PlayerWeakStopState(this);
        playerDashStopState = new PlayerDashStopState(this);
        playerJumpState = new PlayerJumpState(this);
        playerDashState = new PlayerDashState(this);
        playerStrongStopState = new PlayerStrongStopState(this);
        playerFallingState = new PlayerFallingState(this);
        playerSoftLandingState = new PlayerSoftLandingState(this);
        playerHardLandingState = new PlayerHardLandingState(this);
        playerPlungeLandingState = new PlayerPlungeLandingState(this);
        playerSprintState = new PlayerSprintState(this);
        playerPlungeState = new PlayerPlungeState(this);
        playerAttackState = new PlayerAttackState(this);
        playerDeadState = new PlayerDeadState(this);


        StartState(playerIdleState);
    }

    public void OnEnable()
    {
        StateMachineManager.OnEnable();
        StateMachineManager.ResetState();
    }

    public void OnDisable()
    {
        StateMachineManager.OnDisable();
    }

    public void OnDestroy()
    {
        OnDisable();
    }
}
