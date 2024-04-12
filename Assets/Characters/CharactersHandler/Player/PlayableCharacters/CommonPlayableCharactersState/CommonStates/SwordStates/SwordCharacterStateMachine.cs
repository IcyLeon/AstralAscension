using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterStateMachine : PlayableCharacterStateMachine
{
    public SwordIdleState swordIdleState { get; protected set; }
    public SwordCharacterStateMachine(Characters characters) : base(characters)
    {
        swordIdleState = new SwordIdleState(this);
        ChangeState(swordIdleState);
    }
}
