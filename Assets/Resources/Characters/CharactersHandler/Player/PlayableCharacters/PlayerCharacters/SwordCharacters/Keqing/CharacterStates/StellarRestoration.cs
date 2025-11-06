using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellarRestoration : ElementalSkillStateMachine
{
    public PlayerAimController playerAimController { get; }
    public KeqingAimState AimState()
    {
        return new KeqingAimState(this);
    }

    public KeqingTeleportState TeleportState()
    {
        return new KeqingTeleportState(this);
    }

    public KeqingThrowState ThrowState()
    {
        return new KeqingThrowState(this);
    }

    public KeqingESlashState OneSlashState()
    {
        return new KeqingESlashState(this);
    }

    public StellarRestorationReusableData stellarRestorationReusableData { get; private set; }

    protected override void ElementalSkill_started()
    {
        if (stellarRestorationReusableData.CanThrow())
            return;

        playableCharacterStateMachine.ChangeState(TeleportState());
    }

    protected override void ElementalSkill_canceled()
    {
        if (!stellarRestorationReusableData.CanThrow())
            return;

        Vector3 origin = playableCharacterStateMachine.playableCharacter.GetCenterBound();
        stellarRestorationReusableData.SetTargetPosition(origin + playableCharacterStateMachine.playableCharacter.transform.forward * stellarRestorationReusableData.ElementalSkillRange);
        playableCharacterStateMachine.ChangeState(ThrowState());
    }

    protected override void ElementalSkill_performed()
    {
        if (!stellarRestorationReusableData.CanThrow())
            return;

        playableCharacterStateMachine.ChangeState(AimState());
    }

    public KeqingAnimationSO keqingAnimationSO
    {
        get
        {
            return playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO as KeqingAnimationSO;
        }
    }

    protected override SkillReusableData CreateSkillReusableData()
    {
        if (stellarRestorationReusableData == null)
        {
            stellarRestorationReusableData = new StellarRestorationReusableData(this);
        }

        return stellarRestorationReusableData;
    }


    public StellarRestoration(PlayableCharacterStateMachine PlayableCharacterStateMachine) : base(PlayableCharacterStateMachine)
    {
        if (keqingAnimationSO == null)
        {
            Debug.LogError("To use Stellar Restoration skill, make sure it has KeqingAnimationSO scriptable object!");
        }
        playerAimController = new PlayerAimController(playableCharacterStateMachine.playableCharacter);
    }
}
