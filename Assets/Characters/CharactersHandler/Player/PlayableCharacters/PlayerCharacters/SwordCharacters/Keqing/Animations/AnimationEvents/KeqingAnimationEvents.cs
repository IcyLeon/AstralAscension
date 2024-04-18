using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAnimationEvents : SwordCharacterAnimationEvents
{
    [SerializeField] private Transform EmitterPivot;
    [SerializeField] private Transform Mesh;
    [SerializeField] private Transform Armature;

    public Keqing keqing { 
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
        HairpinTeleporter HT = keqing.hairpinTeleporter;

        if (HT == null)
            return;

        KeqingReuseableData keqingReuseableData = playableCharacters.characterReuseableData as KeqingReuseableData;
        HT.transform.SetParent(EmitterPivot);
        HT.transform.localPosition = Vector3.zero;
        HT.SetTargetLocation(keqingReuseableData.targetPosition);
    }
}
