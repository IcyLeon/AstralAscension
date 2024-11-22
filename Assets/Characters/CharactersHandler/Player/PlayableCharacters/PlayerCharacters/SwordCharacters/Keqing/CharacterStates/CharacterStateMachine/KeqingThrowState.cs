using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingThrowState : KeqingElementalSkillState
{
    public KeqingThrowState(SkillStateMachine Skill) : base(Skill)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stellarRestoration.stellarRestorationReusableData.OnHairPinShoot += KeqingAnimationEvents_OnHairPinShoot;
        playableCharacterStateMachine.playerStateMachine.playerController.playerInputAction.Look.Disable();
        SetAnimationTrigger(stellarRestoration.keqingAnimationSO.throwParameter);

    }

    private void KeqingAnimationEvents_OnHairPinShoot(object sender, EventArgs e)
    {
        if (playableCharacters.playerCharactersSO == null)
            return;

        playableCharacters.PlayVOAudio(playableCharacters.playerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomElementalSkillVOClip());
    }

    public override void Exit()
    {
        base.Exit();
        playableCharacterStateMachine.playerController.playerInputAction.Look.Enable();
        stellarRestoration.stellarRestorationReusableData.OnHairPinShoot -= KeqingAnimationEvents_OnHairPinShoot;
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        playableCharacterStateMachine.player.CameraManager.ToggleAimCamera(false, 0.08f);

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
    }
}
