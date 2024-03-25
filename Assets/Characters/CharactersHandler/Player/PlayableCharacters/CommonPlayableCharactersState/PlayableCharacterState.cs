using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterStateMachine : CharacterStateMachine
{
    private PlayerStateMachine playerStateMachine;

    public Player player { 
        get
        {
            return playableCharacters.player;
        }
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

    public PlayableCharacterStateMachine(Characters characters) : base(characters)
    {
        playerStateMachine = new PlayerStateMachine(this);


    }


    public PlayableCharacters playableCharacters
    {
        get
        {
            return (PlayableCharacters)characters;
        }
    }
}
