using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingESlashState : StellarRestorationState
{
    public KeqingESlashState(StellarRestoration StellarRestoration) : base(StellarRestoration)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(stellarRestoration.keqingAnimationSO.elementalSkillSlashParameter);
        playableCharacter.PlayVOAudio(playableCharacter.playerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomElementalSkill_RecastVOClip());
        ResetVelocity();
    }

    private void ResetVelocity()
    {
        playableCharacterStateMachine.ResetVelocity();
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerIdleState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        SmoothRotateToTargetRotation();
    }

    public override void Exit()
    {
        base.Exit();
        playableCharacterStateMachine.player.Rb.useGravity = true;

        SkillBurstManager.AddState(this);
    }
}
