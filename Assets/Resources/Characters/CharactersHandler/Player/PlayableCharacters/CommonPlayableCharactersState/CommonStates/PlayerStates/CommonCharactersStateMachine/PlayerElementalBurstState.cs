using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerElementalBurstState : PlayerElementalState
{
    public PlayerElementalBurstState(SkillStateMachine Skill) : base(Skill)
    {
    }

    public override void Enter()
    {
        base.Enter();
        InitBaseBurstAction();
    }

    protected virtual void InitBaseBurstAction()
    {
        playableCharacter.PlayVOAudio(playableCharacter.playerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomElementalBurstVOClip());
        playableCharacter.playableCharacterDataStat.ResetElementalBurstCooldown();
        SetAnimationTrigger(playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.elementalStateHash.elementalBurstParameter);
    }
}