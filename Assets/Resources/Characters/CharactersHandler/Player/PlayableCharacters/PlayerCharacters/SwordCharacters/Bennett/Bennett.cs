using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bennett : PlayableCharacters
{
    protected override CharacterStateMachine GetStateMachine()
    {
        return new KeqingStateMachine(this);
    }
}
