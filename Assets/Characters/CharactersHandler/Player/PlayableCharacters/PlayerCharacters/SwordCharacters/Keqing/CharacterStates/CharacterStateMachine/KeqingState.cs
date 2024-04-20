using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeqingState : SwordState
{
    public KeqingState(CharacterStateMachine CharacterStateMachine) : base(CharacterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        keqingStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
    }

    protected override void Attack_performed(InputAction.CallbackContext obj)
    {
        base.Attack_performed(obj);

        if (keqingStateMachine.playerStateMachine.IsInState<PlayerAirborneState>())
            return;

        keqingStateMachine.ChangeState(keqingStateMachine.keqingAttackState);
    }

    public override void Exit()
    {
        base.Exit();
        keqingStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
    }

    protected override void ElementalSkill_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalState())
            return;

        if (keqingStateMachine.keqing.activehairpinTeleporter == null ||
            !keqingStateMachine.keqing.activehairpinTeleporter.CanTeleport())
            return;

        keqingStateMachine.ChangeState(keqingStateMachine.keqingTeleportState);
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalState())
            return;

        if (keqingStateMachine.keqing.activehairpinTeleporter != null)
            return;

        float Range = keqingStateMachine.playableCharacters.PlayerCharactersSO.ElementalSkillRange;
        Vector3 origin = keqingStateMachine.playableCharacters.GetMiddleBound();
        keqingStateMachine.keqingReuseableData.targetPosition = origin + keqingStateMachine.playableCharacters.transform.forward * Range;
        keqingStateMachine.ChangeState(keqingStateMachine.keqingThrowState);
    }


    protected override void ElementalBurst_performed(InputAction.CallbackContext obj)
    {
    }

    protected override void ElementalSkill_performed(InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalState())
            return;

        if (keqingStateMachine.keqing.activehairpinTeleporter != null)
            return;

        keqingStateMachine.ChangeState(keqingStateMachine.keqingAimState);
    }

    protected KeqingStateMachine keqingStateMachine
    {
        get
        {
            return (KeqingStateMachine)playableCharacterStateMachine;
        }
    }
}
