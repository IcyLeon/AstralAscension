using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeqingTeleportState : StellarRestorationState
{
    private float TimeToReachElapsed;
    private float TimeToReach, MaxTimeToReach;
    private const float Speed = 35f;
    private const float OffsetTime = 0.5f;
    private Vector3 dir;

    public KeqingTeleportState(SkillStateMachine Skill) : base(Skill)
    {
    }

    public override void Enter()
    {
        dir = GetDirectionToTeleporter();
        TimeToReach = dir.magnitude / Speed;
        MaxTimeToReach = (Range + Range * OffsetTime) / Speed;
        TimeToReachElapsed = Time.time;
        float angle = Vector3Handler.FindAngleByDirection(Vector2.zero, new Vector2(dir.x, dir.z));
        UpdateTargetRotationData(angle);

        playableCharacterStateMachine.player.Rb.useGravity = false;
        playableCharacterStateMachine.playableCharacter.Animator.gameObject.SetActive(false);
    }

    public override void OnEnable()
    {
    }

    public override void OnDisable()
    {
    }

    private Vector3 GetDirectionToTeleporter()
    {
        return stellarRestoration.stellarRestorationReusableData.targetPosition - playableCharacterStateMachine.player.Rb.position;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        UpdateTeleportMovement();
        SmoothRotateToTargetRotation();
    }

    public override void Update()
    {
        base.Update();

        if (Time.time - TimeToReachElapsed >= TimeToReach || Time.time - TimeToReachElapsed >= MaxTimeToReach)
        {
            TransitToSlash();
            return;
        }

        stellarRestoration.stellarRestorationReusableData.hairpinTeleporter.ResetTime();
    }

    private void UpdateTeleportMovement()
    {
        playableCharacterStateMachine.player.Rb.MovePosition(playableCharacterStateMachine.player.Rb.position + Speed * Time.fixedDeltaTime * dir.normalized);
    }

    public override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
        TransitToSlash();
    }

    private void TransitToSlash()
    {
        playableCharacterStateMachine.ChangeState(stellarRestoration.keqingESlashState);
    }

    public override void Exit()
    {
        base.Exit();
        stellarRestoration.stellarRestorationReusableData.hairpinTeleporter.Hide();
        playableCharacterStateMachine.playableCharacter.Animator.gameObject.SetActive(true);
    }
}
