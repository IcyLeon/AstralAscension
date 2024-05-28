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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
    }

    protected override void ElementalSkill_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalState())
            return;

        if (keqingStateMachine.keqingReuseableData.CanThrow())
            return;

        playableCharacterStateMachine.ChangeState(keqingStateMachine.keqingTeleportState);
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalState())
            return;

        if (!keqingStateMachine.keqingReuseableData.CanThrow())
            return;

        Vector3 origin = playableCharacterStateMachine.playableCharacters.GetCenterBound();
        keqingStateMachine.keqingReuseableData.targetPosition = origin + playableCharacterStateMachine.playableCharacters.transform.forward * keqingStateMachine.keqingReuseableData.Range;
        playableCharacterStateMachine.ChangeState(keqingStateMachine.keqingThrowState);
    }


    protected override void ElementalBurst_performed(InputAction.CallbackContext obj)
    {
    }

    protected override void ElementalSkill_performed(InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalState())
            return;

        if (!keqingStateMachine.keqingReuseableData.CanThrow())
            return;

        playableCharacterStateMachine.ChangeState(keqingStateMachine.keqingAimState);
    }

    protected KeqingStateMachine keqingStateMachine
    {
        get
        {
            return (KeqingStateMachine)swordCharacterStateMachine;
        }
    }
}
