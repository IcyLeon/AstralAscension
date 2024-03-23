using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterState : CharacterState
{
    private PlayerState playerState;

    public Player player { 
        get
        {
            return playableCharacters.player;
        }
    }

    public override void Update()
    {
        if (playerState != null)
            playerState.Update();

        base.Update();
    }

    public override void FixedUpdate()
    {
        if (playerState != null)
            playerState.FixedUpdate();

        base.FixedUpdate();
    }

    public PlayableCharacterState(Characters characters) : base(characters)
    {
        playerState = new PlayerState(this);


    }


    private PlayableCharacters playableCharacters
    {
        get
        {
            return (PlayableCharacters)characters;
        }
    }
}
