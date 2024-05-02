using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingESlashState : KeqingElementalSkillState
{
    public KeqingESlashState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(keqingStateMachine.keqingAnimationSO.elementalSkillSlashParameter);
        playableCharacterStateMachine.playableCharacters.PlayVOAudio(keqing.PlayerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomElementalSkill_RecastVOClip());
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
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
    }
}
