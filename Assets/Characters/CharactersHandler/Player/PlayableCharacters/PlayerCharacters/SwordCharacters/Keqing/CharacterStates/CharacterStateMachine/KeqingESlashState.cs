using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingESlashState : KeqingElementalSkillState
{
    public KeqingESlashState(SkillStateMachine Skill) : base(Skill)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(stellarRestoration.keqingAnimationSO.elementalSkillSlashParameter);
        playableCharacters.PlayVOAudio(playableCharacters.playerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomElementalSkill_RecastVOClip());
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

        SkillBurstManager.AddState(this);
    }
}
