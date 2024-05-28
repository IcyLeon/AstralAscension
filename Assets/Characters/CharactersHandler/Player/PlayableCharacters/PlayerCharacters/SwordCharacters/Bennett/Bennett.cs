using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bennett : PlayableCharacters
{
    protected override CharacterStateMachine CreateCharacterStateMachine()
    {
        return new KeqingStateMachine(this);
    }
}
