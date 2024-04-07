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
        keqingStateMachine.keqing.TargetOrb.SetActive(true);
        keqingStateMachine.keqing.AimRig.SetTargetWeight(1f);
        StartAnimation(keqingStateMachine.keqingAnimationSO.aimParameter);
        playableCharacterStateMachine.playerStateMachine.ChangeState(
            playableCharacterStateMachine.playerStateMachine.playerAimState
            );

    }
    protected override void SubscribeInputs()
    {
        base.SubscribeInputs();
        keqingStateMachine.player.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        keqingStateMachine.ChangeState(keqingStateMachine.keqingThrowState);
    }

    protected override void UnsubscribeInputs()
    {
        base.UnsubscribeInputs();
        playableCharacterStateMachine.player.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;

    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        Vector3 origin = keqingStateMachine.playableCharacters.MainCollider.bounds.center;
        Vector3 originalPos = Player.GetTargetCameraRayPosition(Range + GetOffSet(origin));
        keqingStateMachine.keqingReuseableData.targetPosition = Player.GetRayPosition(origin,
                                                            originalPos - origin, 
                                                            Range);
        keqingStateMachine.keqing.TargetOrb.transform.position = keqingStateMachine.keqingReuseableData.targetPosition;
    }

    private float GetOffSet(Vector3 EmitterPos)
    {
        return Mathf.Sqrt((keqingStateMachine.player.CameraManager.CameraMain.transform.position - EmitterPos).magnitude);
    }

    public override void Exit()
    {
        base.Exit();
        keqingStateMachine.keqing.TargetOrb.SetActive(false);
        keqingStateMachine.keqing.AimRig.SetTargetWeight(0f);
        StopAnimation(keqingStateMachine.keqingAnimationSO.aimParameter);
    }
}
