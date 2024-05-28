using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingStateMachine : SwordCharacterStateMachine
{
    public KeqingAimState keqingAimState { get; }
    public KeqingThrowState keqingThrowState { get; }
    public KeqingTeleportState keqingTeleportState { get; }
    public KeqingESlashState keqingESlashState { get; }
    public KeqingAnimationSO keqingAnimationSO
    {
        get
        {
            return (KeqingAnimationSO)playableCharacters.PlayableCharacterAnimationSO;
        }
    }

    public KeqingReuseableData keqingReuseableData
    {
        get
        {
            return (KeqingReuseableData)playableCharacterReuseableData;
        }
    }


    protected override void InitState()
    {
        EntityState = new KeqingState(this);
    }

    public KeqingStateMachine(Characters characters) : base(characters)
    {
        characterReuseableData = new KeqingReuseableData(2, this);
        keqingAimState = new KeqingAimState(this);
        keqingThrowState = new KeqingThrowState(this);
        keqingTeleportState = new KeqingTeleportState(this);
        keqingESlashState = new KeqingESlashState(this);
        playerCharacterAttackState = new KeqingAttackState(this);
    }
}
