using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBurst
{
    bool isBurstEnded();
    void UpdateBurst();
}

public abstract class PlayerElementalBurstState : PlayerElementalState, IBurst
{
    public PlayerElementalBurstState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playableCharacters.PlayVOAudio(playableCharacters.playerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomElementalBurstVOClip());
        SetAnimationTrigger(playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.elementalStateHash.elementalBurstParameter);
    }

    public void UpdateBurst()
    {

    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
    }

    public bool isBurstEnded()
    {
        return false;
    }
}