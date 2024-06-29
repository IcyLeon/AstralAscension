using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterStateMachine : DamageableCharacterStateMachine
{
    public PlayerStateMachine playerStateMachine { get; }
    public PlayerCharacterAttackState playerCharacterAttackState { get; protected set; }
    public PlayableCharacterPlungeAttackState playableCharacterPlungeAttackState { get; protected set; }
    public ElementalBurst playerElementalBurst { get; protected set; }
    public ElementalSkill playerElementalSkill { get; protected set; }

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

    public override void Update()
    {
        if (playerStateMachine != null)
            playerStateMachine.Update();

        Debug.Log(StateMachineManager.currentStates);

        UpdateSkillData();
        UpdateBurstData();

        base.Update();
    }

    protected abstract void InitSkills();

    protected override void InitState()
    {
        base.InitState();
        InitSkills();
    }

    private void UpdateSkillData()
    {
        if (playerElementalSkill == null || playerElementalSkill.skillReusableData == null)
            return;

        playerElementalSkill.skillReusableData.Update();
    }

    private void UpdateBurstData()
    {
        if (playerElementalBurst == null || playerElementalBurst.skillReusableData == null)
            return;

        playerElementalBurst.skillReusableData.Update();
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
        OnDestroySkillData();
        OnDestroyBurstData();
        OnDisable();
    }

    private void OnDestroySkillData()
    {
        if (playerElementalSkill == null || playerElementalSkill.skillReusableData == null)
            return;

        playerElementalSkill.skillReusableData.OnDestroy();
    }

    private void OnDestroyBurstData()
    {
        if (playerElementalBurst == null || playerElementalBurst.skillReusableData == null)
            return;

        playerElementalBurst.skillReusableData.OnDestroy();
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
