using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeqingTeleportState : KeqingElementalSkillState
{
    private float TimeToReachElapsed;
    private float TimeToReach, MaxTimeToReach;
    private const float Speed = 25f;
    private const float OffsetTime = 0.5f;
    private Vector3 dir;

    public KeqingTeleportState(Skill Skill) : base(Skill)
    {
    }

    public override void Enter()
    {
        TimeToReachElapsed = 0;

        dir = GetDirectionToTeleporter();
        TimeToReach = dir.magnitude / Speed;
        MaxTimeToReach = (Range + Range * OffsetTime) / Speed;

        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        UpdateTargetRotationData(angle);

        playableCharacterStateMachine.player.Rb.useGravity = false;
        playableCharacterStateMachine.playableCharacters.Animator.gameObject.SetActive(false);
    }

    public override void OnEnable()
    {
    }

    public override void OnDisable()
    {
    }

    private Vector3 GetDirectionToTeleporter()
    {
        return stellarRestoration.stellarRestorationReusableData.hairpinTeleporter.transform.position - playableCharacterStateMachine.player.Rb.position;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        UpdateTeleportMovement();
        SmoothRotateToTargetRotation();
    }

    private void ResetVelocity()
    {
        playableCharacterStateMachine.player.Rb.velocity = Vector3.zero;
    }

    public override void Update()
    {
        base.Update();

        if (TimeToReachElapsed >= TimeToReach || TimeToReachElapsed >= MaxTimeToReach)
        {
            TransitToSlash();
            return;
        }

        stellarRestoration.stellarRestorationReusableData.hairpinTeleporter.ResetTime();
        TimeToReachElapsed += Time.deltaTime;
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
        ResetVelocity();

        playableCharacterStateMachine.ChangeState(stellarRestoration.keqingESlashState);
    }

    public override void Exit()
    {
        base.Exit();
        stellarRestoration.stellarRestorationReusableData.hairpinTeleporter.Hide();
        playableCharacterStateMachine.playableCharacters.Animator.gameObject.SetActive(true);
    }
}
