using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingStateMachine : SwordCharacterStateMachine
{
    public KeqingAimState keqingAimState { get; }
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
        playerElementalBurstState = new KeqingElementalBurstState(this);
        playerElementalSkillState = new KeqingThrowState(this);
        playerCharacterAttackState = new KeqingAttackState(this);
        playableCharacterPlungeAttackState = new KeqingPlungeAttackState(this);
        keqingAimState = new KeqingAimState(this);
        keqingTeleportState = new KeqingTeleportState(this);
        keqingESlashState = new KeqingESlashState(this);
    }
}
