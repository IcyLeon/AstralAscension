using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public PlayableCharacterAttackStateMachine playableCharacterAttackStateMachine { get; protected set; }
    public ElementalBurstStateMachine playerElementalBurstStateMachine { get; protected set; }
    public ElementalSkillStateMachine playerElementalSkillStateMachine { get; protected set; }
    public PlayableCharacters playableCharacter { get; }
    public Player player { get; }
    public PlayerData playerData { get; private set; }

    public PlayableCharacterReuseableData playableCharacterReuseableData
    {
        get
        {
            return characterReuseableData as PlayableCharacterReuseableData;
        }
    }

    public bool IsAirborne()
    {
        return StateMachineManager.IsInState<PlayerAirborneState>();
    }

    public override void Update()
    {
        base.Update();

        playerElementalSkillStateMachine.Update();

        playerElementalBurstStateMachine.Update();

        playableCharacterAttackStateMachine.Update();

        playerData.Update();
    }

    protected abstract void InitSkills();

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        playerElementalBurstStateMachine.FixedUpdate();

        playerElementalSkillStateMachine.FixedUpdate();

        playableCharacterAttackStateMachine.FixedUpdate();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        playerElementalBurstStateMachine.LateUpdate();

        playerElementalSkillStateMachine.LateUpdate();

        playableCharacterAttackStateMachine.LateUpdate();
    }

    public void ResetVelocity()
    {
        player.Rb.velocity = Vector3.zero;
    }


    public PlayableCharacterStateMachine(Characters characters) : base(characters)
    {        
        playableCharacter = damageableCharacters as PlayableCharacters;
        player = playableCharacter.player;
        playerData = PlayerData.instance;
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
        playableCharacterAttackStateMachine = new PlayableCharacterAttackStateMachine(this);

        InitSkills();
        StartState(playerIdleState);
    }

    public override void OnEnable()
    {
        base.OnEnable();

        playerElementalBurstStateMachine.OnEnable();

        playerElementalSkillStateMachine.OnEnable();

        playableCharacterAttackStateMachine.OnEnable();

        playableCharacter.OnTakeDamage += PlayableCharacter_OnTakeDamage;
    }

    private void PlayableCharacter_OnTakeDamage(float DamageAmount)
    {
        SetAnimationTrigger("Hit");
    }

    public override void OnDisable()
    {
        base.OnDisable();

        playerElementalBurstStateMachine.OnDisable();

        playerElementalSkillStateMachine.OnDisable();

        playableCharacterAttackStateMachine.OnDisable();

        playableCharacter.OnTakeDamage -= PlayableCharacter_OnTakeDamage;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        playerElementalBurstStateMachine.OnDestroy();

        playerElementalSkillStateMachine.OnDestroy();

        playableCharacterAttackStateMachine.OnDestroy();

        OnDisable();
    }


    public bool IsSkillCasting()
    {
        return IsInState<PlayerElementalState>();
    }
}
