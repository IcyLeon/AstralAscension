using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAnimationEvents : SwordCharacterAnimationEvents
{
    [SerializeField] private Transform EmitterPivot;

    private Keqing keqing { 
        get 
        {
            return (Keqing)playableCharacters;
        }
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
