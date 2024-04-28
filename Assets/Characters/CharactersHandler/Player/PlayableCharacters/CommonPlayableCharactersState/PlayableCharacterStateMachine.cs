using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterStateMachine : CharacterStateMachine
{
    public PlayerStateMachine playerStateMachine { get; }
    public PlayerCharacterAttackState playerCharacterAttackState { get; protected set; }
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

    public override bool IsAttacking()
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

    public bool IsSkillCasting()
    {
        return IsInState<PlayerElementalState>();
    }
}
