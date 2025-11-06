using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keqing : PlayableCharacters
{
    
    protected override PlayableCharacterStateMachine CreatePlayableCharacterStateMachine()
    {
        return new KeqingStateMachine(this);
    }
}
