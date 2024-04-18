using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAimState : KeqingElementalSkillState
{
    private float Range;
    public KeqingAimState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
        Range = keqingStateMachine.playableCharacters.PlayerCharactersSO.ElementalSkillRange;
    }

    public override void Enter()
    {
        base.Enter();
        keqing.TargetOrb.SetActive(true);
        keqing.AimRig.SetTargetWeight(1f);
        StartAnimation(keqingStateMachine.keqingAnimationSO.aimParameter);
        playableCharacterStateMachine.playerStateMachine.ChangeState(
            playableCharacterStateMachine.playerStateMachine.playerAimState
            );

    }
    public override void SubscribeInputs()
    {
        base.SubscribeInputs();
        keqingStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        keqingStateMachine.ChangeState(keqingStateMachine.keqingThrowState);
    }

    public override void UnsubscribeInputs()
    {
        base.UnsubscribeInputs();
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;

    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        Vector3 origin = keqingStateMachine.playableCharacters.GetMiddleBound();
        Vector3 originalTargetPos = Player.GetTargetCameraRayPosition(Range + GetOffSet(origin));
        keqingStateMachine.keqingReuseableData.targetPosition = Player.GetRayPosition(origin,
                                                            originalTargetPos - origin, 
                                                            Range);
        keqing.TargetOrb.transform.position = keqingStateMachine.keqingReuseableData.targetPosition;
    }

    private float GetOffSet(Vector3 EmitterPos)
    {
        return (keqingStateMachine.player.CameraManager.CameraMain.transform.position - EmitterPos).magnitude;
    }

    public override void Exit()
    {
        base.Exit();
        keqing.TargetOrb.SetActive(false);
        keqing.AimRig.SetTargetWeight(0f);
        StopAnimation(keqingStateMachine.keqingAnimationSO.aimParameter);
    }
}
