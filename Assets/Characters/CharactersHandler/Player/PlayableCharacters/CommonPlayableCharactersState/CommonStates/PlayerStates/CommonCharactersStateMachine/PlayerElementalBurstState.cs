using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerElementalBurstState : PlayerElementalState
{
    public PlayerElementalBurstState(Skill Skill) : base(Skill)
    {
    }

    public override void Enter()
    {
        base.Enter();
        InitBaseBurstAction();
    }

    protected virtual void InitBaseBurstAction()
    {
        playableCharacters.PlayVOAudio(playableCharacters.playerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomElementalBurstVOClip());
        playableCharacters.playableCharacterDataStat.ResetElementalBurstCooldown();
        SetAnimationTrigger(playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.elementalStateHash.elementalBurstParameter);
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        TransitBurstState();
    }

    public ElementalBurst elementalBurst
    {
        get
        {
            return skill as ElementalBurst;
        }
    }

    protected virtual void TransitBurstState()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
    }
}