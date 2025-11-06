using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAimState : StellarRestorationState
{
    private PlayerAimController playerAimController;
    public KeqingAimState(StellarRestoration StellarRestoration) : base(StellarRestoration)
    {
        playerAimController = StellarRestoration.playerAimController;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stellarRestoration.keqingAnimationSO.aimParameter);
        stellarRestoration.stellarRestorationReusableData.ToggleTargetOrb(true);
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

        playableCharacterStateMachine.ChangeState(stellarRestoration.ThrowState());
    }

    public override void Update()
    {
        base.Update();

        Vector3 origin = playableCharacter.GetCenterBound();
        Vector3 originalTargetPos = playableCharacterStateMachine.player.playerCameraManager.GetTargetCameraRayPosition(Range + GetOffSet(origin));
        Vector3 targetPos = Player.GetRayPosition(origin, originalTargetPos - origin, Range);
        stellarRestoration.stellarRestorationReusableData.SetTargetPosition(targetPos);
        playerAimController.SetTargetPosition(targetPos);
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
        stellarRestoration.stellarRestorationReusableData.ToggleTargetOrb(false);
    }
}
