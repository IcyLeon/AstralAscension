using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterData : CharacterData
{
    public PlayerCharactersSO playerCharactersSO { 
        get 
        { 
            return (PlayerCharactersSO)charactersSO; 
        } 
    }
    public PlayableCharacterData(CharactersSO charactersSO) : base(charactersSO)
    {
    }
}
