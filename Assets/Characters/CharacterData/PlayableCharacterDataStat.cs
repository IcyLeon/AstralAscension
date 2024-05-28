using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterDataStat : CharacterDataStat
{
    private int currentAscension;

    public PlayerCharactersSO playerCharactersSO { 
        get 
        { 
            return (PlayerCharactersSO)charactersSO; 
        } 
    }

    public PlayableCharacterDataStat(CharactersSO charactersSO, int currentAscension = 0) : base(charactersSO)
    {
        this.currentAscension = currentAscension;
    }
}
