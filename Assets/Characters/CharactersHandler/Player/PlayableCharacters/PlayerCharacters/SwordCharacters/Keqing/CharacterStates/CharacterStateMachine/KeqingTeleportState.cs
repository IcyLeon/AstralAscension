using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeqingTeleportState : KeqingElementalSkillState
{
    private float TimeToReachElapsed;
    private float TimeToReach;
    private const float Speed = 25f;
    private float Range;

    private Vector3 originPosition;

    public KeqingTeleportState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
        Range = keqingStateMachine.playableCharacters.PlayerCharactersSO.ElementalSkillRange;
    }

    public override void Enter()
    {
        TimeToReachElapsed = 0;

        Vector3 dir = GetDirectionToTeleporter();
        TimeToReach = dir.magnitude / Speed;
        originPosition = playableCharacterStateMachine.player.Rb.position;

        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        UpdateTargetRotationData(angle);

        playableCharacterStateMachine.player.Rb.useGravity = false;
        keqingStateMachine.keqing.CharacterModelTransform.gameObject.SetActive(false);
    }

    private Vector3 GetDirectionToTeleporter()
    {
        return keqingStateMachine.keqingReuseableData.activehairpinTeleporter.transform.position - playableCharacterStateMachine.player.Rb.position;
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

        Vector3 dir = originPosition - playableCharacterStateMachine.player.Rb.position;

        if (dir.magnitude >= Range || TimeToReachElapsed >= TimeToReach)
        {
            TransitToSlash();
            return;
        }

        keqingStateMachine.keqingReuseableData.activehairpinTeleporter.ResetTime();
        TimeToReachElapsed += Time.deltaTime;
    }

    private void UpdateTeleportMovement()
    {
        playableCharacterStateMachine.player.Rb.MovePosition(playableCharacterStateMachine.player.Rb.position + Speed * Time.deltaTime * GetDirectionToTeleporter().normalized);
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
        keqingStateMachine.keqingReuseableData.activehairpinTeleporter.Hide();
        keqingStateMachine.keqing.CharacterModelTransform.gameObject.SetActive(true);
    }
}
