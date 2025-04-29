using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAimState : StellarRestorationState
{
    private AimRigController aimRigController;
    private TargetOrb targetOrb;
    public KeqingAimState(SkillStateMachine Skill) : base(Skill)
    {
        aimRigController = playableCharacterStateMachine.playableCharacter.GetComponentInChildren<AimRigController>();
        targetOrb = playableCharacterStateMachine.playableCharacter.GetComponentInChildren<TargetOrb>();
        ToggleTargetOrb(false);
        if (aimRigController == null)
        {
            Debug.LogError("Dont have AimRigController!");
        }
    }
    private void ToggleTargetOrb(bool active)
    {
        targetOrb.gameObject.SetActive(active);
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stellarRestoration.keqingAnimationSO.aimParameter);
        ToggleTargetOrb(true);
        playerAimController.Enter();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playableCharacterStateMachine.player.playerController.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        playableCharacterStateMachine.player.playerController.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
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

        Vector3 origin = playableCharacter.GetCenterBound();
        Vector3 originalTargetPos = playableCharacterStateMachine.player.playerCameraManager.GetTargetCameraRayPosition(Range + GetOffSet(origin));
        Vector3 targetPos = Player.GetRayPosition(origin, originalTargetPos - origin, Range);
        stellarRestoration.stellarRestorationReusableData.SetTargetPosition(targetPos);
        aimRigController.SetTargetPosition(stellarRestoration.stellarRestorationReusableData.GetTargetOrbPosition());
        playerAimController.Update();
    }

    private float GetOffSet(Vector3 EmitterPos)
    {
        return (playableCharacterStateMachine.player.playerCameraManager.cameraMain.transform.position - EmitterPos).magnitude;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stellarRestoration.keqingAnimationSO.aimParameter);
        ToggleTargetOrb(false);
    }
}
