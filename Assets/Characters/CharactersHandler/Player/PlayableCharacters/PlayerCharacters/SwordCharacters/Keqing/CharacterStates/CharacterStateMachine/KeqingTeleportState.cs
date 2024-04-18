using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeqingTeleportState : KeqingElementalSkillState
{
    private float TimeToReached = 0.1f;
    private Transform activeTeleporterTransform;

    public delegate void OnKeqingTeleport(bool enter);
    public static OnKeqingTeleport OnKeqingTeleportState;

    public KeqingTeleportState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    public override void Enter()
    {
        activeTeleporterTransform = keqing.activehairpinTeleporter.transform;
        playableCharacterStateMachine.playerStateMachine.playerData.rotationTime = 0.02f;
        keqing.player.Rb.useGravity = false;
        OnKeqingTeleportState?.Invoke(true);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        SmoothRotateToTargetRotation();
    }

    public override void Update()
    {
        base.Update();

        Vector3 dir = activeTeleporterTransform.position - keqing.player.Rb.position;

        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        UpdateTargetRotationData(angle);

        if (dir.magnitude <= 1.5f)
        {
            TransitToSlash();
            return;
        }

        float speed = dir.magnitude / TimeToReached;
        keqing.player.Rb.position = Vector3.MoveTowards(keqing.player.Rb.position, activeTeleporterTransform.position, Time.deltaTime * speed);
        keqing.activehairpinTeleporter.ResetTime();

    }

    public override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
        TransitToSlash();
    }

    private void TransitToSlash()
    {
        keqingStateMachine.ChangeState(keqingStateMachine.keqingESlashState);
    }

    public override void Exit()
    {
        base.Exit();
        keqing.activehairpinTeleporter.Hide();
        OnKeqingTeleportState?.Invoke(false);
    }
}
