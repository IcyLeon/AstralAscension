using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingThrowState : StellarRestorationState
{
    public KeqingThrowState(SkillStateMachine Skill) : base(Skill)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stellarRestoration.stellarRestorationReusableData.OnHairPinShoot += KeqingAnimationEvents_OnHairPinShoot;
        playableCharacterStateMachine.player.playerController.playerInputAction.Look.Disable();
        SetAnimationTrigger(stellarRestoration.keqingAnimationSO.throwParameter);

    }

    private void KeqingAnimationEvents_OnHairPinShoot(object sender, EventArgs e)
    {
        if (playableCharacter.playerCharactersSO == null)
            return;

        playableCharacter.PlayVOAudio(playableCharacter.playerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomElementalSkillVOClip());
    }

    public override void Exit()
    {
        base.Exit();
        playableCharacterStateMachine.player.playerController.playerInputAction.Look.Enable();
        stellarRestoration.stellarRestorationReusableData.OnHairPinShoot -= KeqingAnimationEvents_OnHairPinShoot;
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        playerAimController.Exit(0.08f);

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerIdleState);
    }
}
