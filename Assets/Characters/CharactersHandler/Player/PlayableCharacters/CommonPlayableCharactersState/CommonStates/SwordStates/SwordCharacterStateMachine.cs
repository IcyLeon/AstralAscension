using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterStateMachine : PlayableCharacterStateMachine
{
    protected override void InitState()
    {
        EntityState = new SwordState(this);
    }

    public SwordCharacterStateMachine(Characters characters) : base(characters)
    {
    }
}
