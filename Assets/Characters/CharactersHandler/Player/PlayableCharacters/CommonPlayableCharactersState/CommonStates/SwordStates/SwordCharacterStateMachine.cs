using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterStateMachine : PlayableCharacterStateMachine
{
    public SwordState swordState { get; protected set; }
    public SwordCharacterStateMachine(Characters characters) : base(characters)
    {
    }
}
