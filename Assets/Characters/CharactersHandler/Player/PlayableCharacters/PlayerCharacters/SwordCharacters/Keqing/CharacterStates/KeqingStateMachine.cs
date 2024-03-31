using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingStateMachine : SwordCharacterStateMachine
{
    public KeqingAimState keqingAimState { get; }

    public KeqingStateMachine(Characters characters) : base(characters)
    {
        swordState = new KeqingState(this);
        keqingAimState = new KeqingAimState(this);
        ChangeState(swordState);
    }
}
