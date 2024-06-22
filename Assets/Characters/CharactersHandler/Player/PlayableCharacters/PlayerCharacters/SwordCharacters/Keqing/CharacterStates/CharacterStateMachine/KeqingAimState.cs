using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAimState : KeqingElementalSkillState
{
    private AimRigController aimRigController;

    public KeqingAimState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
        aimRigController = keqingStateMachine.keqingReuseableData.aimRigController;
        if (aimRigController == null)
        {
            Debug.LogError("Dont have AimRigController!");
        }
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

    public override void OnDisable()
    {
        base.OnDisable();
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!keqingStateMachine.keqingReuseableData.CanThrow())
            return;

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerElementalSkillState);
    }

    public override void Update()
    {
        base.Update();

        Vector3 origin = playableCharacters.GetCenterBound();
        Vector3 originalTargetPos = Player.GetTargetCameraRayPosition(Range + GetOffSet(origin));
        keqingStateMachine.keqingReuseableData.targetPosition = Player.GetRayPosition(origin,
                                                            originalTargetPos - origin,
                                                            Range);
        aimRigController.SmoothRigTransition.SetTargetPosition(keqingStateMachine.keqingReuseableData.targetPosition);
    }

    private float GetOffSet(Vector3 EmitterPos)
    {
        return (playableCharacterStateMachine.player.CameraManager.CameraMain.transform.position - EmitterPos).magnitude;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(keqingStateMachine.keqingAnimationSO.aimParameter);
        aimRigController.SmoothRigTransition.ToggleTarget(false);
    }
}
