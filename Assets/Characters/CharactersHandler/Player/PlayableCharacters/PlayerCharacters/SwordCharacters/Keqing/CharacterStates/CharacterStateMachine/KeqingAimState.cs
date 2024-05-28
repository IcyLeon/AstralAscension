using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAimState : KeqingElementalSkillState
{
    public KeqingAimState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(keqingStateMachine.keqingAnimationSO.aimParameter);

        playableCharacterStateMachine.playerStateMachine.ChangeState(playableCharacterStateMachine.
            playerStateMachine.playerAimState);
    }
    public override void OnEnable()
    {
        base.OnEnable();
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        keqingStateMachine.ChangeState(keqingStateMachine.keqingThrowState);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
    }

    public override void Update()
    {
        base.Update();

        Vector3 origin = keqingStateMachine.playableCharacters.GetCenterBound();
        Vector3 originalTargetPos = Player.GetTargetCameraRayPosition(Range + GetOffSet(origin));
        keqingStateMachine.keqingReuseableData.targetPosition = Player.GetRayPosition(origin,
                                                            originalTargetPos - origin,
                                                            Range);
        aimRigController.SetTargetPosition(keqingStateMachine.keqingReuseableData.targetPosition);
    }

    private float GetOffSet(Vector3 EmitterPos)
    {
        return (keqingStateMachine.player.CameraManager.CameraMain.transform.position - EmitterPos).magnitude;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(keqingStateMachine.keqingAnimationSO.aimParameter);
        aimRigController.SmoothRigTransition.ToggleTarget(false);
    }
}
