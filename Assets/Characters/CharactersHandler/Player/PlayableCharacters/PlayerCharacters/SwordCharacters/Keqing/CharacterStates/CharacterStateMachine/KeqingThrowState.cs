using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingThrowState : KeqingElementalSkillState
{
    public KeqingThrowState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    public override void Enter()
    {
        base.Enter();
        KeqingAnimationEvents.OnHairPinShoot += KeqingAnimationEvents_OnHairPinShoot;
        playableCharacterStateMachine.playerStateMachine.player.PlayerController.playerInputAction.Look.Disable();
        SetAnimationTrigger(keqingStateMachine.keqingAnimationSO.throwParameter);

    }

    private void KeqingAnimationEvents_OnHairPinShoot(HairpinTeleporter HairpinTeleporter)
    {
        if (playableCharacters.playerCharactersSO == null)
            return;

        playableCharacters.PlayVOAudio(playableCharacters.playerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomElementalSkillVOClip());
    }

    public override void Exit()
    {
        base.Exit();
        playableCharacters.player.PlayerController.playerInputAction.Look.Enable();
        KeqingAnimationEvents.OnHairPinShoot -= KeqingAnimationEvents_OnHairPinShoot;
    }
    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        playableCharacterStateMachine.player.CameraManager.ToggleAimCamera(false, 0.08f);

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
    }
}
