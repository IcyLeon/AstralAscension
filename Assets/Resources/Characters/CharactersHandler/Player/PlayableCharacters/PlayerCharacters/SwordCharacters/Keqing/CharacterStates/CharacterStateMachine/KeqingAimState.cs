using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAimState : StellarRestorationState
{
    private AimRigController aimRigController;

    public KeqingAimState(SkillStateMachine Skill) : base(Skill)
    {
        aimRigController = stellarRestoration.stellarRestorationReusableData.aimRigController;
        if (aimRigController == null)
        {
            Debug.LogError("Dont have AimRigController!");
        }
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stellarRestoration.keqingAnimationSO.aimParameter);
        playerAimController.Enter();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playableCharacterStateMachine.playerController.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        playableCharacterStateMachine.playerController.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        playerAimController.FixedUpdate();
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!stellarRestoration.stellarRestorationReusableData.CanThrow())
            return;

        playableCharacterStateMachine.ChangeState(stellarRestoration.keqingThrowState);
    }

    public override void Update()
    {
        base.Update();

        Vector3 origin = playableCharacters.GetCenterBound();
        Vector3 originalTargetPos = Player.GetTargetCameraRayPosition(Range + GetOffSet(origin));
        stellarRestoration.stellarRestorationReusableData.targetPosition = Player.GetRayPosition(origin,
                                                            originalTargetPos - origin,
                                                            Range);
        aimRigController.SmoothRigTransition.SetTargetPosition(stellarRestoration.stellarRestorationReusableData.targetPosition);
        playerAimController.Update();
    }

    private float GetOffSet(Vector3 EmitterPos)
    {
        return (playableCharacterStateMachine.player.PlayerCameraManager.CameraMain.transform.position - EmitterPos).magnitude;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stellarRestoration.keqingAnimationSO.aimParameter);
        aimRigController.SmoothRigTransition.ToggleTarget(false);
    }
}
