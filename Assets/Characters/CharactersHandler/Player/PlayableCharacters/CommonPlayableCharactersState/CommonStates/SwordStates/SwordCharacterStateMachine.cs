using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterStateMachine : PlayableCharacterStateMachine
{
    public SwordState swordState { get; protected set; }

    protected virtual void InitState()
    {
        swordState = new SwordState(this);
    }

    public SwordCharacterStateMachine(Characters characters) : base(characters)
    {
        InitState();
        ChangeState(swordState);
    }
}
