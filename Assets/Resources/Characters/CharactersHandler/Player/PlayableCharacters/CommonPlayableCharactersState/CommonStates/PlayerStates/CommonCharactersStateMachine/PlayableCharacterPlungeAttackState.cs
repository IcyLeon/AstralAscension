using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterPlungeAttackState : PlayerCharacterState
{
    public PlayableCharacterPlungeAttackState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        DamageNearbyTargets();
    }

    private void DamageNearbyTargets()
    {
        Collider[] colliders = Physics.OverlapSphere(playableCharacterStateMachine.player.Rb.position, 5f);

        foreach(var collider in colliders)
        {
            IDamageable IDamageable = collider.GetComponent<IDamageable>();

            if (IDamageable == null || IDamageable is PlayableCharacters)
                continue;

            OnPlungeAttack(IDamageable, playableCharacterStateMachine.playableCharacters, collider.ClosestPoint(playableCharacterStateMachine.player.Rb.position));
        }
    }

    private void OnPlungeAttack(IDamageable target, IAttacker source, Vector3 HitPosition = default(Vector3))
    {
        if (target == null)
            return;

        PlungeAttackTarget(target, source, HitPosition);
    }

    protected virtual void PlungeAttackTarget(IDamageable target, IAttacker source, Vector3 HitPosition = default(Vector3))
    {
        target.TakeDamage(source, null, 1f, HitPosition);
    }

    protected override void OnPlungeUpdate()
    {
        if (playableCharacterStateMachine.playerStateMachine == null)
            return;

        if (!playableCharacterStateMachine.playerStateMachine.IsInState<PlayerPlungeState>())
        {
            playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
            return;
        }
    }

}
