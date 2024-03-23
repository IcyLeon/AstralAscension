using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterData : CharacterData
{
    private int currentAscension;

    public PlayerCharactersSO playerCharactersSO { 
        get 
        { 
            return (PlayerCharactersSO)charactersSO; 
        } 
    }

    public PlayableCharacterData(CharactersSO charactersSO, int currentAscension = 0) : base(charactersSO)
    {
        this.currentAscension = currentAscension;
    }
}
