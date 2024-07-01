using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellarRestoration : ElementalSkillStateMachine
{
    public KeqingAimState keqingAimState { get; }
    public KeqingTeleportState keqingTeleportState { get; }
    public KeqingThrowState keqingThrowState { get; }
    public KeqingESlashState keqingESlashState { get; }

    public StellarRestorationReusableData stellarRestorationReusableData
    {
        get
        {
            return skillReusableData as StellarRestorationReusableData;
        }
    }

    public KeqingAnimationSO keqingAnimationSO
    {
        get
        {
            return playableCharacterStateMachine.playableCharacters.PlayableCharacterAnimationSO as KeqingAnimationSO;
        }
    }

    public override void InitElementalSkillState()
    {
        skillReusableData = new StellarRestorationReusableData(this); 
        elementalSkillControlBaseState = new KeqingElementalSkillControlState(this);
    }

    public StellarRestoration(PlayableCharacterStateMachine PlayableCharacterStateMachine) : base(PlayableCharacterStateMachine)
    {
        if (keqingAnimationSO == null)
        {
            Debug.LogError("To use Stellar Restoration skill, make sure it has KeqingAnimationSO scriptable object!");
        }
        keqingAimState = new KeqingAimState(this);
        keqingTeleportState = new KeqingTeleportState(this);
        keqingThrowState = new KeqingThrowState(this);
        keqingESlashState = new KeqingESlashState(this);
    }
}
