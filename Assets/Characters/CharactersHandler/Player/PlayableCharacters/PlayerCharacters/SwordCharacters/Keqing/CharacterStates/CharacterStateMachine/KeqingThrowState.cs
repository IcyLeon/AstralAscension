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
        HairpinTeleporter.OnHairPinThrow += ActivehairpinTeleporter_OnHairPinThrow;
        SetAnimationTrigger(keqingStateMachine.keqingAnimationSO.throwParameter);

    }

    private void ActivehairpinTeleporter_OnHairPinThrow()
    {
        keqing.PlayVOAudio(keqing.PlayerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomElementalSkillVOClip());
    }

    public override void Exit()
    {
        base.Exit();
        HairpinTeleporter.OnHairPinThrow -= ActivehairpinTeleporter_OnHairPinThrow;
    }
    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        keqingStateMachine.player.CameraManager.ToggleAimCamera(false, 0.08f);

        keqingStateMachine.ChangeState(keqingStateMachine.EntityState);
    }
}
