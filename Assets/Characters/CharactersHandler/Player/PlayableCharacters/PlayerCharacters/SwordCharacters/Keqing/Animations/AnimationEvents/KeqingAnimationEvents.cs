using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAnimationEvents : SwordCharacterAnimationEvents
{
    [SerializeField] private Transform EmitterPivot;
    [SerializeField] private Transform Mesh;
    [SerializeField] private Transform Armature;

    private Keqing keqing { 
        get 
        {
            return (Keqing)playableCharacters;
        }
    }

    private void Awake()
    {
        KeqingTeleportState.OnKeqingTeleportState += OnKeqingTeleportState;
    }

    private void OnDestroy()
    {
        KeqingTeleportState.OnKeqingTeleportState -= OnKeqingTeleportState;
    }

    private void OnKeqingTeleportState(bool enter)
    {
        Mesh.gameObject.SetActive(!enter);
        Armature.gameObject.SetActive(!enter);
    }

    private void ShootTeleporter()
    {
        HairpinTeleporter HT = keqing.keqingReuseableData.hairpinTeleporter;

        if (HT == null)
            return;

        HT.transform.SetParent(EmitterPivot);
        HT.transform.localPosition = Vector3.zero;
        HT.SetTargetLocation(keqing.keqingReuseableData.targetPosition);
    }
}
