using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalBurst : Skill
{
    public PlayerElementalBurstState playerElementalBurstState { get; protected set; } // all characters will play an animation before transition to other states
    public ElementalBurstControlState elementalBurstControlBaseState { get; protected set; }

    public abstract void InitElementalBurstState();
    public ElementalBurst(PlayableCharacterStateMachine playableCharacterStateMachine) : base(playableCharacterStateMachine)
    {
        InitElementalBurstState();
    }
}
