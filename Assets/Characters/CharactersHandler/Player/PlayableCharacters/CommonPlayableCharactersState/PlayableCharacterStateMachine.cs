using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterStateMachine : CharacterStateMachine
{
    public PlayerStateMachine playerStateMachine { get; }
    public PlayerCharacterAttackState playerCharacterAttackState { get; protected set; }
    public PlayableCharacterPlungeAttackState playableCharacterPlungeAttackState { get; protected set; }
    public PlayerElementalBurstState playerElementalBurstState { get; protected set; }
    public PlayableCharacters playableCharacters
    {
        get
        {
            return (PlayableCharacters)characters;
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
            return (PlayableCharacterReuseableData)characterReuseableData;
        }
    }

    public bool IsAttacking()
    {
        return StateMachineManager.IsInState<PlayerCharacterAttackState>();
    }

    public override void Update()
    {
        if (playerStateMachine != null)
            playerStateMachine.Update();

        base.Update();
    }


    public override void FixedUpdate()
    {
        if (playerStateMachine != null)
            playerStateMachine.FixedUpdate();

        base.FixedUpdate();
    }

    public override void LateUpdate()
    {
        if (playerStateMachine != null)
            playerStateMachine.LateUpdate();

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
        {
            playerStateMachine.OnDestroy();
        }
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
