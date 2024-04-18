using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeqingTeleportState : KeqingElementalSkillState
{
    private Transform activeTeleporterTransform;

    public delegate void OnKeqingTeleport(bool enter);
    public static OnKeqingTeleport OnKeqingTeleportState;

    public KeqingTeleportState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    public override void Enter()
    {
        activeTeleporterTransform = keqing.activehairpinTeleporter.transform;
        keqing.player.Rb.useGravity = false;
        OnKeqingTeleportState?.Invoke(true);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        UpdateTeleportMovement();
        SmoothRotateToTargetRotation();
    }

    private void ResetVelocity()
    {
        keqing.player.Rb.velocity = Vector3.zero;
    }

    public override void Update()
    {
        base.Update();

        Vector3 dir = activeTeleporterTransform.position - keqing.player.Rb.position;

        if (dir.magnitude <= 1.5f)
        {
            TransitToSlash();
            return;
        }

        keqing.activehairpinTeleporter.ResetTime();

    }

    private void UpdateTeleportMovement()
    {
        Vector3 dir = activeTeleporterTransform.position - keqing.player.Rb.position;

        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        UpdateTargetRotationData(angle);

        keqing.player.Rb.MovePosition(keqing.player.Rb.position + 25f * Time.deltaTime * dir.normalized);
    }

    public override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
        TransitToSlash();
    }

    private void TransitToSlash()
    {
        ResetVelocity();
        keqingStateMachine.ChangeState(keqingStateMachine.keqingESlashState);
    }

    public override void Exit()
    {
        base.Exit();
        keqing.activehairpinTeleporter.Hide();
        OnKeqingTeleportState?.Invoke(false);
    }
}
