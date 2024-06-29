using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeqingState : SwordState
{
    public KeqingState(CharacterStateMachine CharacterStateMachine) : base(CharacterStateMachine)
    {
    }

    //protected override void ElementalSkill_started()
    //{
    //    if (keqingStateMachine.keqingReuseableData.CanThrow())
    //        return;

    //    playableCharacterStateMachine.ChangeState(keqingStateMachine.keqingTeleportState);
    //}

    protected override void OnSwordHitDamage(PlayableCharacterSwordHitCollider.PlayableCharacterHitEvents e)
    {
        e.damageable.TakeDamage(e.source, e.source.GetElementsSO(), 1f, e.hitPosition);
    }

    //protected override void ElementalSkill_canceled()
    //{
    //    if (!keqingStateMachine.keqingReuseableData.CanThrow())
    //        return;

    //    Vector3 origin = playableCharacterStateMachine.playableCharacters.GetCenterBound();
    //    keqingStateMachine.keqingReuseableData.targetPosition = origin + playableCharacterStateMachine.playableCharacters.transform.forward * keqingStateMachine.keqingReuseableData.ElementalSkillRange;
    //    playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerElementalSkillState);
    //}

    //protected override void ElementalSkill_performed()
    //{
    //    if (!keqingStateMachine.keqingReuseableData.CanThrow())
    //        return;

    //    playableCharacterStateMachine.ChangeState(keqingStateMachine.keqingAimState);
    //}

    protected KeqingStateMachine keqingStateMachine
    {
        get
        {
            return (KeqingStateMachine)swordCharacterStateMachine;
        }
    }
}
