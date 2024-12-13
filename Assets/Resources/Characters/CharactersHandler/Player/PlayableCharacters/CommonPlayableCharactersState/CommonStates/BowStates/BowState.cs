using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BowState : PlayerCharacterState
{
    protected BowState(CharacterStateMachine CharacterStateMachine) : base(CharacterStateMachine)
    {
    }
}
