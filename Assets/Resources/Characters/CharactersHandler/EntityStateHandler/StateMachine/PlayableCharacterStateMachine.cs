using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterStateMachine : DamageableCharacterStateMachine
{
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
    public PlayerDeadState playerDeadState { get; }

    public PlayerCharacterAttackState playerCharacterAttackState { get; protected set; }
    public ElementalBurstStateMachine playerElementalBurstStateMachine { get; protected set; }
    public ElementalSkillStateMachine playerElementalSkillStateMachine { get; protected set; }
    public PlayerController playerController { get; private set; }
    public PlayableCharacters playableCharacter { get; }
    public Player player { get; }

    public PlayableCharacterReuseableData playableCharacterReuseableData
    {
        get
        {
            return characterReuseableData as PlayableCharacterReuseableData;
        }
    }

    public bool IsAttacking()
    {
        return StateMachineManager.IsInState<PlayerCharacterAttackState>();
    }

    public bool IsAirborne()
    {
        return StateMachineManager.IsInState<PlayerAirborneState>();
    }


    public override void Update()
    {
        base.Update();
        //Debug.Log(StateMachineManager.currentStates);

        if (playerElementalSkillStateMachine != null)
            playerElementalSkillStateMachine.Update();

        if (playerElementalBurstStateMachine != null)
            playerElementalBurstStateMachine.Update();
    }

    protected abstract void InitSkills();

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (playerElementalBurstStateMachine != null)
            playerElementalBurstStateMachine.FixedUpdate();

        if (playerElementalSkillStateMachine != null)
            playerElementalSkillStateMachine.FixedUpdate();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        if (playerElementalBurstStateMachine != null)
            playerElementalBurstStateMachine.LateUpdate();

        if (playerElementalSkillStateMachine != null)
            playerElementalSkillStateMachine.LateUpdate();

    }

    public void ResetVelocity()
    {
        player.Rb.velocity = Vector3.zero;
    }


    public PlayableCharacterStateMachine(Characters characters) : base(characters)
    {        
        playerController = PlayerController.instance;
        playableCharacter = damageableCharacters as PlayableCharacters;
        player = playableCharacter.player;
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
        playerDeadState = new PlayerDeadState(this);


        InitSkills();
        StartState(playerIdleState);
    }

    public override void OnEnable()
    {
        base.OnEnable();

        if (playerElementalBurstStateMachine != null)
            playerElementalBurstStateMachine.OnEnable();

        if (playerElementalSkillStateMachine != null)
            playerElementalSkillStateMachine.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (playerElementalBurstStateMachine != null)
            playerElementalBurstStateMachine.OnDisable();

        if (playerElementalSkillStateMachine != null)
            playerElementalSkillStateMachine.OnDisable();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        if (playerElementalBurstStateMachine != null)
            playerElementalBurstStateMachine.OnDestroy();

        if (playerElementalSkillStateMachine != null)
            playerElementalSkillStateMachine.OnDestroy();

        OnDisable();
    }




    //private void ActiveCharacter_OnPlayerCharacterSwitch(CharacterDataStat playerData, PlayableCharacters playableCharacters)
    //{
    //    ChangeState(EntityState);
    //    OnDisable();
    //}

    public bool IsSkillCasting()
    {
        return IsInState<PlayerElementalState>();
    }
}
