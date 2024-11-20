using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterStateMachine : DamageableCharacterStateMachine
{
    public PlayerStateMachine playerStateMachine { get; }
    public PlayerCharacterAttackState playerCharacterAttackState { get; protected set; }
    public PlayableCharacterPlungeAttackState playableCharacterPlungeAttackState { get; protected set; }
    public ElementalBurstStateMachine playerElementalBurstStateMachine { get; protected set; }
    public ElementalSkillStateMachine playerElementalSkillStateMachine { get; protected set; }

    public PlayableCharacters playableCharacters
    {
        get
        {
            return damageableCharacters as PlayableCharacters;
        }
    }
    public Player player { 
        get
        {
            return playableCharacters.player;
        }
    }
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
        if (playerStateMachine != null)
            playerStateMachine.Update();

        //Debug.Log(StateMachineManager.currentStates);

        if (playerElementalSkillStateMachine != null)
            playerElementalSkillStateMachine.Update();

        if (playerElementalBurstStateMachine != null)
            playerElementalBurstStateMachine.Update();

        base.Update();
    }

    protected abstract void InitSkills();

    protected override void InitState()
    {
        base.InitState();
        InitSkills();
    }

    public override void FixedUpdate()
    {
        if (playerStateMachine != null)
            playerStateMachine.FixedUpdate();

        if (playerElementalBurstStateMachine != null)
            playerElementalBurstStateMachine.FixedUpdate();

        if (playerElementalSkillStateMachine != null)
            playerElementalSkillStateMachine.FixedUpdate();

        base.FixedUpdate();
    }

    public override void LateUpdate()
    {
        if (playerStateMachine != null)
            playerStateMachine.LateUpdate();

        if (playerElementalBurstStateMachine != null)
            playerElementalBurstStateMachine.LateUpdate();

        if (playerElementalSkillStateMachine != null)
            playerElementalSkillStateMachine.LateUpdate();

        base.LateUpdate();
    }

    public PlayableCharacterStateMachine(Characters characters) : base(characters)
    {
        playerStateMachine = new PlayerStateMachine(this);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (playerStateMachine != null)
        {
            playerStateMachine.OnEnable();
        }
        ActiveCharacter.OnPlayerCharacterExit += ActiveCharacter_OnPlayerCharacterSwitch;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (playerStateMachine != null)
        {
            playerStateMachine.OnDisable();
        }
        ActiveCharacter.OnPlayerCharacterExit -= ActiveCharacter_OnPlayerCharacterSwitch;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        if (playerStateMachine != null)
            playerStateMachine.OnDestroy();

        if (playerElementalBurstStateMachine != null)
            playerElementalBurstStateMachine.OnDestroy();

        if (playerElementalSkillStateMachine != null)
            playerElementalSkillStateMachine.OnDestroy();

        OnDisable();
    }




    private void ActiveCharacter_OnPlayerCharacterSwitch(CharacterDataStat playerData, PlayableCharacters playableCharacters)
    {
        ChangeState(EntityState);
        OnDisable();
    }

    public bool IsSkillCasting()
    {
        return IsInState<PlayerElementalState>();
    }
}
