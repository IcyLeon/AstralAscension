using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingPlungeAttackState : PlayableCharacterPlungeAttackState
{
    public KeqingPlungeAttackState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }

    protected override void PlungeAttackTarget(IDamageable target, IAttacker source, Vector3 HitPosition = default(Vector3))
    {
        target.TakeDamage(source, playableCharacterStateMachine.playableCharacters.GetElementsSO(), 100f, HitPosition);
    }
}
